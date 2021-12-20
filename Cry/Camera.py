import cv2 as cv
import numpy as np
import serial
import time
import enum


class RobotStates(enum.Enum):
    MoveToInitial = 0,
    ScanningPrintedObject = 1,
    Magnetize = 2,
    MovingToSetPoint = 3,
    ScanningBanner = 4,
    DeMagnetize = 5,

cap = cv.VideoCapture('/dev/video0')
arduino = serial.Serial('/dev/ttyUSB0', 115200, timeout=0.1)

currentRobotState = RobotStates.MoveToInitial

map = {
    3: 'Triangle',
    4: 'Square',
    15: 'Circle'
}

def empty(val):
    pass
map = {
    3: 'Triangle',
    4: 'Square',
    15: 'Circle'
}
def Write(text):
    arduino.write(bytes(text, 'utf-8'))

def IsReached():
    line = arduino.readline()
    return line == b'reached\r\n'

def GetText(points):
    if points in map.keys():
        return map[points]
    if points >= 15:
        return map[15]
    return ""

#9500 is max height
def MoveToInitialPosition():
    Write('0,0,2000\n')

def init():
    cv.namedWindow('mask')
    cv.createTrackbar('Min h', 'mask', 0, 255, empty)
    cv.setTrackbarPos('Min h', 'mask', 60)

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
    cv.createTrackbar('Min area', 'contour', 0, 255, empty)
    cv.setTrackbarPos('Min area', 'contour', 100)


time.sleep(2)

while True:
    
    if currentRobotState == RobotStates.MoveToInitial:
        
        if IsReached():
            currentRobotState = RobotStates.ScanningPrintedObject
            continue
        
        MoveToInitialPosition()

        pass

    elif currentRobotState == RobotStates.ScanningPrintedObject:
        
        print("Yay")
        
        pass
    elif currentRobotState == RobotStates.Magnetize:
        pass
    elif currentRobotState == RobotStates.MovingToSetPoint:
        pass
    elif currentRobotState == RobotStates.ScanningBanner:
        pass
    elif currentRobotState == RobotStates.DeMagnetize:
        pass


init()
MoveToInitialPosition()
magnetize = False

while True:
    _, capturedImg = cap.read()
    if capturedImg is None:
        continue
    
    #cv.imshow('original', capturedImg)
    
    blur = cv.blur(capturedImg, (3, 3))
    #cv.imshow('blur', blur)

    capturedImgHSV = cv.cvtColor(blur, cv.COLOR_BGR2HSV)

    minh = cv.getTrackbarPos('Min h', 'mask')
    mins = cv.getTrackbarPos('Min s', 'mask')
    minv = cv.getTrackbarPos('Min v', 'mask')
    
    maxh = cv.getTrackbarPos('Max h', 'mask')
    maxs = cv.getTrackbarPos('Max s', 'mask')
    maxv = cv.getTrackbarPos('Max v', 'mask')
    
    mask = cv.inRange(capturedImgHSV, (minh, mins, minv), (maxh, maxs, maxv))
    cv.imshow('mask', mask)

    grayScale = mask

    kernel = np.ones((5,5), np.uint8)
    dilate = cv.dilate(grayScale, kernel, iterations=1)
    
    #cv.imshow('dilate', dilate)

    mincanny = cv.getTrackbarPos('Min canny', 'mask')
    maxcanny = cv.getTrackbarPos('Max canny', 'mask')
    
    canny = cv.Canny(dilate, mincanny, maxcanny)
    #cv.imshow('canny', canny)

    _, contours, _ = cv.findContours(canny, cv.RETR_TREE, cv.CHAIN_APPROX_SIMPLE)

    contourImg = capturedImg.copy()
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

        arcLength = cv.arcLength(cnt, closed)
        approx = cv.approxPolyDP(cnt, 0.01 * arcLength, closed)

        moments = cv.moments(cnt)
        x = int(moments['m10'] / moments['m00'])
        y = int(moments['m01'] / moments['m00'])

        text = GetText(len(approx))
        if text == "":
            continue

        fullText = f"{text} ({x}, {y})"
        cv.putText(shapeContours, fullText, (x, y), cv.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0))

        Text = ""

        height, width, channel = capturedImg.shape

        magnetize = True
    
        cv.drawContours(shapeContours, contours, i, (255, 0, 0), 3)

    cv.imshow('Shapes contour', shapeContours)

    #if magnetize:
        #Write('mn\n')
    #else:
        #Write('mf\n')
        

    if cv.waitKey(1) & 0xFF == ord('q'):
        break

cv.destroyAllWindows()
    