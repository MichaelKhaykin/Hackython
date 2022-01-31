import cv2 as cv
import numpy as np
import serial
import time
import enum
import math

cap = cv.VideoCapture('/dev/video0')

while True:

    _, capturedImg = cap.read()
    if capturedImg is None:
        continue

    rotatedImage = cv.rotate(capturedImg, 0)
    capturedImg = cv.rotate(rotatedImage, 0)
    

    cv.imshow('test', capturedImg)

    if cv.waitKey(1) & 0xFF == ord('q'):
        break