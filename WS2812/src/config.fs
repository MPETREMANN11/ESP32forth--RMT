\ *************************************
\ configuration 
\    Filename:      config.fs
\    Date:          08 feb 2026
\    Updated:       08 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/config.fs

\ *** RMT General constants and values *****************************************

\ WS2812B timing constants (FastLED proven values)
\ 80MHz RMT clock = 12.5ns per tick
32 constant T0H_TICKS   \ 0 code high time (400ns)
64 constant T1H_TICKS   \ 1 code high time (800ns)  
52 constant TL_TICKS    \ Both low times (650ns average)

 0 constant RMT_MODE_TX     \ for TX mode
 1 constant RMT_MODE_RX     \ for RX mode

18 constant RMT_GPIO        \ using GPIO 18

 2 constant RMT_CHANNEL     \ choice RMT channel

\ *** LEDs constants and values ************************************************

60 constant NB_LEDS         \ defined for ring with 60 LEDs
create LEDS                 \ array for these LEDs
    NB_LEDS RGB * allot     \ 
0 value BRIGHTNESS          \ LEDs brightness 
0 value RMT_CHANNEL         \ RMT channel


<EOF>


