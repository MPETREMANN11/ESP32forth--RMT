\ *************************************
\ clock with ESP32 Fastled
\    Filename:      clockLed.fs
\    Date:          06 feb 2026
\    Updated:       06 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/clockLed.fs

structures also

\ define CLOCK structure for seconds, hours....
struct CLOCK_struct
    RGB_STRUCT field ->rgb
            i8 field ->intensity

only FORTH

\ get values from CLOCK address
: CLOCK@ { addr -- r g b intensity }
    addr ->g c@
    addr ->r c@
    addr ->b c@
    addr ->intensity c@
  ;

\ Define CLOCK color
: CLOCKcolor: ( comp: r g b intensity -- <name> | exec: -- r g b intensity )
    create
        >r >r >r c,
        r> c,
        r> c,
        r> c,
    does>
        CLOCK@
  ;

\ *** Set initial colors for minutes and hours *********************************

RGB_Blue 1 CLOCKcolor: CLOCK_minutes
RGB_Blue 8 CLOCKcolor: CLOCK_hours

\ couronne en bleu très faible pour marquer les minutes
: setInitMinutes ( -- )
    CLOCK_minutes setIntensity drop drop drop
    60 0 do
       CLOCK_minutes drop i nLED!
    loop
  ;

\ couronne en bleu très faible pour marquer les minutes
: setInitHours ( -- )
    CLOCK_hours setIntensity drop drop drop
    60 0 do
         CLOCK_hours drop i nLED!
    5 +loop
  ;

\ *** Set HH MM SS in clock ****************************************************

\ calculate HH position on clock
: calc-led-hh  { hh mm -- iLed }
    hh 12 mod 5 *       \ Calcul de la base (ex: 2h -> 10)
    mm 6 + 12 /         \ Calcul de l'avance des minutes avec arrondi
    + 60 mod            \ Addition et sécurité cycle 60
  ;

RGB_Cyan   20 CLOCKcolor: CLOCK_SS
RGB_Green  20 CLOCKcolor: CLOCK_MM
RGB_red    20 CLOCKcolor: CLOCK_HH

: setSS { sec -- }
    CLOCK_SS setIntensity
    sec nLED!
  ;

: setMM { min -- }
    CLOCK_MM setIntensity
    min nLED!
  ;

: setHH { hour min -- }
    hour min calc-led-hh { position }
    CLOCK_HH setIntensity
    position nLED!
  ;

: setHMS ( -- )
    RTC.get-time
    setSS
    dup setMM
    setHH
  ;

: displayClock ( -- )
    begin 
        resetLEDs
        setInitMinutes
        setInitHours
        setHMS
        transmitLEDS
        1000 ms
    again
  ;

\ tasks 
\ : hi   begin ." Time is: " ms-ticks . cr 1000 ms again ; 
\ ' hi 100 100 task my-counter 
\ my-counter start-task 

<EOF>

