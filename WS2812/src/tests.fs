\ *************************************
\ tests for ESP32 Fastled
\    Filename:      tests.fs
\    Date:          06 feb 2026
\    Updated:       14 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/tests.fs

\ resetLEDs
\ 10 setIntensity
\ RGB_Black 0 nLED!
\ RGB_Red 1 nLED!
\ RGB_Green 2 nLED!
\ RGB_Blue 3 nLED!
\ RGB_Yellow 4 nLED!
\ RGB_Cyan 5 nLED!
\ RGB_Magenta 6 nLED!
\ RGB_White 7 nLED!
\ RGB_Orange 8 nLED!
\ RGB_Purple 9 nLED!
\ RGB_Pink 10 nLED!
\ 
\ transmitLEDS
\ LEDS hex . decimal
\ LEDS 20 dump
\ 
\ 
\ resetLEDs
\ transmitLEDS

\ tasks 
\ : hi   begin ." Time is: " ms-ticks . cr 1000 ms again ; 
\ ' hi 100 100 task my-counter 
\ my-counter start-task 


22 34 00 RTC.set-time { hh mm ss -- }
' displayClock 100 100 task my-clock
my-clock start-task

<EOF>


