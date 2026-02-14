\ *************************************
\ real time for ESP32 Fastled
\    Filename:      realTime.fs
\    Date:          14 feb 2026
\    Updated:       14 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/realTime.fs

0 value currentTime 
 
\ store current time 
: RTC.set-time { hh mm ss -- } 
    hh 3600 * 
    mm 60 * 
    ss + +  1000 * 
    MS-TICKS - to currentTime 
  ; 

\ fetch current time in seconds 
: RTC.get-time ( -- hh mm ss ) 
    currentTime MS-TICKS + 1000 / 
    3600 /mod swap 60 /mod swap 
  ; 

\ used for SS and MM part of time display 
: :## ( n -- n' ) 
    #  6 base ! #  decimal  [char] : hold 
  ; 
 
\ display current time 
: RTC.display-time ( -- ) 
    currentTime MS-TICKS + 1000 / 
    <# :## :## 24 MOD #S #> type 
  ; 

<EOF>


