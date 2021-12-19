import cv2 as cv
import numpy as np

cap = cv.VideoCapture('/dev/video0')

minh = 60
maxh = 100

mins = 50
maxs = 255

minv = 50
maxv = 255

mincanny = 180
maxcanny = 120

while True:
    _, capturedImg = cap.read()
    if capturedImg is None:
        continue
    
    cv.imshow('original', capturedImg)
    
    blur = cv.blur(capturedImg, (3, 3))
    cv.imshow('blur', blur)

    capturedImgHSV = cv.cvtColor(blur, cv.COLOR_BGR2HSV)

    mask = cv.inRange(capturedImgHSV, (minh, mins, minv), (maxh, maxs, maxv))
    cv.imshow('mask', mask)

    grayScale = mask

    kernel = np.ones((5,5), np.uint8)
    dilate = cv.dilate(grayScale, kernel, iterations=1)
    
    cv.imshow('dilate', dilate)

    canny = cv.Canny(dilate, mincanny, maxcanny)
    cv.imshow('canny', canny)

    if cv.waitKey(1) & 0xFF == ord('q'):
        break

cv.destroyAllWindows()
    