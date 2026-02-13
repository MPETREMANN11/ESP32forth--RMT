\ *************************************
\ words for ESP32 Fastled
\    Filename:      fastled.fs
\    Date:          06 feb 2026
\    Updated:       13 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


\ exemple en ligne: https://circuitlabs.net/rmt-for-ws2812-neopixel-led-control/ 

RECORDFILE /spiffs/fastleds.fs

: initGPIO ( -- )
    RMT_GPIO OUTPUT pinMode 
  ;

also rmt

0 value tx_channel
0 value encodeHandle

: initWS2812 ( -- )
    initGPIO    \ defined in fastleds.fs
    RMT_GPIO prepare_ws2812_config
    ?dup if
        to tx_channel
    else
        ." erreur initialisation canal RMT" cr
    then
    \ Créer un encodeur à 10 Mhz
    RMT_RES rmt_new_ws2812_encoder  \ Résolution 1MHz sur la pile
                                    \ Retourne le handle de l'encodeur ou 0
    ?dup if                                    
        to encodeHandle
    else
        ." erreur initialisation encodeur RMT" cr
    then

  ;

only FORTH


\ *** Other datas **************************************************************

\ Define named color
: RGBcolor: ( comp: r g b -- <name> | exec: -- r g b )
    create
        >r >r c,
        r> c,
        r> c,
    does>
        RGB@
  ;

  0   0   0 RGBcolor: RGB_Black
255   0   0 RGBcolor: RGB_Red
  0 255   0 RGBcolor: RGB_Green
  0   0 255 RGBcolor: RGB_Blue
255 255   0 RGBcolor: RGB_Yellow
  0 255 255 RGBcolor: RGB_Cyan
255   0 255 RGBcolor: RGB_Magenta
255 255 255 RGBcolor: RGB_White
255 165   0 RGBcolor: RGB_Orange
128   0 128 RGBcolor: RGB_Purple
255 192 203 RGBcolor: RGB_Pink

<EOF>

