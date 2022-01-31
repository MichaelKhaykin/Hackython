import cv2 as cv
import numpy as np
import serial
import time
import enum
import math

def BuildBlur(capturedImg):
    return cv.blur(capturedImg, (3, 3))

def BuildHSV(blur):
    return cv.cvtColor(blur, cv.COLOR_BGR2HSV)

def BuildMask(hsv):
    minh = cv.getTrackbarPos('Min h', 'mask')
    mins = cv.getTrackbarPos('Min s', 'mask')
    minv = cv.getTrackbarPos('Min v', 'mask')
    
    maxh = cv.getTrackbarPos('Max h', 'mask')
    maxs = cv.getTrackbarPos('Max s', 'mask')
    maxv = cv.getTrackbarPos('Max v', 'mask')
    
    return cv.inRange(hsv, (minh, mins, minv), (maxh, maxs, maxv))
  
def BuildDilation(mask):
    kernel = np.ones((5,5), np.uint8)
    dilate = cv.dilate(mask, kernel, iterations=1)
    
    return dilate

def BuildCanny(dilated):
    mincanny = cv.getTrackbarPos('Min canny', 'mask')
    maxcanny = cv.getTrackbarPos('Max canny', 'mask')
    
    canny = cv.Canny(dilated, mincanny, maxcanny)
    
    return canny

def BuildContour(canny):
    _, contours, _ = cv.findContours(canny, cv.RETR_TREE, cv.CHAIN_APPROX_SIMPLE)
    return contours

def GetText(contour, mask):

    #Last paramater is boolean for if shape is closed

    arcLength = cv.arcLength(contour, True)
    approx = cv.approxPolyDP(contour, 0.03 * arcLength, True)

    text = PointsToText(len(approx))

    # check if circle, using this instead of approx poly dp since these are not polygons
    _ ,_,w,_ = cv.boundingRect(contour)
    closeToPi = arcLength / w

    percentError = abs((math.pi - closeToPi)) / math.pi * 100

    if percentError < 8:
        return "Circle"

    #grab the width of the contour, grab the length of the contour and divide
    # that value should be close to pi

  #  if circles is not None:
      #  return circles

    return text

def GetMoments(contour):
    moments = cv.moments(contour)
    x = int(moments['m10'] / moments['m00'])
    y = int(moments['m01'] / moments['m00'])
    return (x, y)


#cv.Mat.BuildBlur = BuildBlur
#cv.Mat.BuildHSV = BuildHSV
#cv.Mat.BuildMask = BuildMask
#cv.Mat.BuildDilation = BuildDilation
#cv.Mat.BuildCanny = BuildCanny
#cv.Mat.BuildContour = BuildContour



class RobotStates(enum.Enum):
    MoveToInitial = 0,
    ScanningPrintedObject = 1,
    Magnetize = 2,
    WaitForTom = 10,
    MoveToSetPoint = 3,
    ScanningBanner = 4,
    MoveToCup = 5,
    DeMagnetize = 6,
    Reset1 = 7,
    Reset2 = 8

cap = cv.VideoCapture('/dev/video0')
arduino = serial.Serial('/dev/ttyUSB0', 115200, timeout=0.1)
#arduino = None

currentRobotState = RobotStates.MoveToInitial

map = {
    3: 'Triangle',
    4: 'Square',
    5: "Star",
    15: 'Circle'
}

def empty(val):
    pass

def Write(text):

    if arduino is None:
        return

    #arduino.flushInput()
    arduino.write(bytes(text, 'utf-8'))

def IsReached():

    if arduino is None:
        return

    line = arduino.readline()
    return line == b'reached\r\n'

def PointsToText(points):
    if points in map.keys():
        return map[points]
    if points >= 15:
        return map[15]
    return ""

#9500 is max height
def MoveToInitialPosition():
    Write('0,0,4500\n')

def init():
    cv.namedWindow('mask')
    cv.createTrackbar('Min h', 'mask', 0, 255, empty)
    cv.setTrackbarPos('Min h', 'mask', 50)

    cv.createTrackbar('Max h', 'mask', 0, 255, empty)
    cv.setTrackbarPos('Max h', 'mask', 100)

    cv.createTrackbar('Min s', 'mask', 0, 255, empty)
    cv.setTrackbarPos('Min s', 'mask', 50)
    
    cv.createTrackbar('Max s', 'mask', 0, 255, empty)
    cv.setTrackbarPos('Max s', 'mask', 255)

    cv.createTrackbar('Min v', 'mask', 0, 255, empty)
    cv.setTrackbarPos('Min v', 'mask', 50)

    cv.createTrackbar('Max v', 'mask', 0, 255, empty)    
    cv.setTrackbarPos('Max v', 'mask', 255)
    
    cv.createTrackbar('Min canny', 'mask', 0, 255, empty)        
    cv.setTrackbarPos('Min canny', 'mask', 180)
    
    cv.createTrackbar('Max canny', 'mask', 0, 255, empty)   
    cv.setTrackbarPos('Max canny', 'mask', 120)

    cv.namedWindow('contour')
    cv.createTrackbar('Min area', 'contour', 1000, 2000, empty)
    cv.setTrackbarPos('Min area', 'contour', 100)

def GetShapeScanned():
    _, capturedImg = cap.read()
    if capturedImg is None:
        return ""

    rotatedImage = cv.rotate(capturedImg, 0)
    capturedImg = cv.rotate(rotatedImage, 0)

    blur = BuildBlur(capturedImg)
    hsv = BuildHSV(blur)
    mask = BuildMask(hsv)
    cv.imshow('mask', mask)
    dilated = BuildDilation(mask)
    canny = BuildCanny(dilated)
    
    contours = BuildContour(canny)
    contourImg = capturedImg.copy() #draw contours over original image (made a copy so we don't modify original)

    minArea = cv.getTrackbarPos('Min area', 'contour')

    shapeDet = "None"

    for i in range(0, len(contours)):
        
        cnt = contours[i]

        if cv.contourArea(cnt) < minArea:
            continue

        text = GetText(cnt, mask)
        if text == "":
            continue

        shapeDet = text

        cv.drawContours(contourImg, contours, i, (0, 255, 0), 2)    

        break

    cv.imshow('contour', contourImg)
    return shapeDet

shapeCount = {
    "Circle": 0,
    "Triangle": 0,
    "Square": 0,
    "Star": 0
}
 
time.sleep(2) #slight delay for stuff to initialize on the hardware side
init()

camPositions = [(31, 73), (83, 67), (120, 80), (172, 74)]
cupPositions = [(44, 61), (88, 67), (134, 65), (184, 62)]
tom = 0

threeDShapeDetected = None


while True:
    
    if currentRobotState == RobotStates.MoveToInitial:
        
        if arduino is None:
            currentRobotState = RobotStates.ScanningPrintedObject
            continue

        if IsReached():

            #reset shape count
            shapeCount = {
                "None": 0,
                "Circle": 0,
                "Triangle": 0,
                "Square": 0,
                "Star": 0
            }

            currentRobotState = RobotStates.ScanningPrintedObject
            continue
        MoveToInitialPosition()

    elif currentRobotState == RobotStates.ScanningPrintedObject:
        
        shapeDetected = GetShapeScanned()

        if shapeDetected != '' and shapeDetected != "None":

            print(f"detected {shapeDetected}")

            shapeCount[shapeDetected] += 1
            if shapeCount[shapeDetected] >= 30:
                currentRobotState = RobotStates.Magnetize

                tom = 0

                threeDShapeDetected = shapeDetected

                shapeCount = {
                    "None": 0,
                    "Circle": 0,
                    "Triangle": 0,
                    "Square": 0,
                    "Star": 0
                }
        
        if cv.waitKey(1) & 0xFF == ord('q'):
            break

    elif currentRobotState == RobotStates.Magnetize:
        
        for i in range(0, 3):
            Write("mn\n")
            
        print("Magentized")
        currentRobotState = RobotStates.WaitForTom
        
    elif currentRobotState == RobotStates.WaitForTom:

        time.sleep(3)
        currentRobotState = RobotStates.MoveToSetPoint
        

    elif currentRobotState == RobotStates.MoveToSetPoint:

        (a1, a2) = camPositions[tom]
        Write(str(a1) + "," + str(a2) + "\n")

        if IsReached():
            currentRobotState = RobotStates.ScanningBanner

    elif currentRobotState == RobotStates.ScanningBanner:
        
        shapeDetected = GetShapeScanned()

        if shapeDetected != "":
        
            print(f"detected {shapeDetected}")
        
            shapeCount[shapeDetected] += 1
            if shapeCount[shapeDetected] >= 15:
                
                if shapeDetected == threeDShapeDetected:
                    currentRobotState = RobotStates.MoveToCup
                else:
                    currentRobotState = RobotStates.MoveToSetPoint
                    tom += 1
    
                shapeCount = {
                    "None": 0,
                    "Circle": 0,
                    "Triangle": 0,
                    "Square": 0,
                    "Star": 0
                }

    
        if cv.waitKey(1) & 0xFF == ord('q'):
            break

    elif currentRobotState == RobotStates.MoveToCup:
        
        (a1, a2) = cupPositions[tom]
        Write(str(a1) + "," + str(a2) + "\n")

        if IsReached():
            currentRobotState = RobotStates.DeMagnetize
            tom += 1
            time.sleep(50 / 1000)


    elif currentRobotState == RobotStates.DeMagnetize:

        for i in range(0, 3):
            Write("mf\n")
            
        print("DeMagentized")
        currentRobotState = RobotStates.Reset1

    elif currentRobotState == RobotStates.Reset1:
        
        Write("0, 90\n")
        
        if(IsReached()):
            currentRobotState = RobotStates.Reset2
            time.sleep(50 / 1000)

    elif currentRobotState == RobotStates.Reset2:

        Write("0, 0\n")
            
        if(IsReached()):
            currentRobotState = RobotStates.ScanningPrintedObject

    
cv.destroyAllWindows()

magnetize = False

while False:
    _, capturedImg = cap.read()
    
    if capturedImg is None:
        continue
    
    #cv.imshow('original', capturedImg)
    
    blur = BuildBlur(capturedImg)
    #cv.imshow('blur', blur)

    hsv = BuildHSV(blur)
    #cv.imshow('hsv', hsv)

    mask = BuildMask(hsv)
    cv.imshow('mask', mask)

    dilated = BuildDilation(mask)
    #cv.imshow('dilate', dilated)

    canny = BuildCanny(dilated)
    #cv.imshow('canny', canny)

    contours = BuildContours(canny)
    contourImg = capturedImg.copy() #draw contours over original image (made a copy so we don't modify original)
    cv.drawContours(contourImg, contours, -1, (0, 255, 0), 2)

    #cv.imshow('contour', contourImg)

    shapeContours = capturedImg.copy()
    
    minArea = cv.getTrackbarPos('Min area', 'contour')

    closed = True
    wentIn = False

    for i in range(0, len(contours)):
        
        cnt = contours[i]

        if cv.contourArea(cnt) < minArea:
            continue
        
        wentIn = True

        text = GetText(cnt)
        if text == "":
            continue
        
        x, y = GetMoments(cnt)

        fullText = f"{text} ({x}, {y})"
        cv.putText(shapeContours, fullText, (x, y), cv.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0))

        height, width, channel = capturedImg.shape

        cv.drawContours(shapeContours, contours, i, (255, 0, 0), 3)

    cv.imshow('Shapes contour', shapeContours)

    if cv.waitKey(1) & 0xFF == ord('q'):
        break

cv.destroyAllWindows()
    