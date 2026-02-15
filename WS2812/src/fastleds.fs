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

\ *** Initialisation RMT *******************************************************

0 value channelHandle
0 value encoderHandle

: initWS2812 ( -- )
    initGPIO    \ defined in fastleds.fs
    RMT_GPIO RMT_RES prepare_ws2812_config
    ?dup if
        to channelHandle
    else
        ." erreur initialisation canal RMT" cr
    then
    \ Créer un encodeur à 10 Mhz
    RMT_RES rmt_new_ws2812_encoder  \ Résolution 1MHz sur la pile
                                    \ Retourne le handle de l'encodeur ou 0
    ?dup if                                    
        to encoderHandle
    else
        ." erreur initialisation encodeur RMT" cr
    then
  ;

\ Define a empty structure for rmt_transmit
create TRANSMIT_CONFIG
    rmt_transmit_config_t allot
    TRANSMIT_CONFIG rmt_transmit_config_t 0 fill
 
only FORTH


\ *** Definition of basic color LEDs sets **************************************

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

\ *** set and manage LEDs array ************************************************

create LEDS                 \ array for these LEDs
    NB_LEDS RGB_struct * allot

\ set all LEDs to zero
: resetLEDs ( -- )
    LEDS NB_LEDS RGB_struct * 0 fill
  ;

\ store LED at position n in LEDS array
: nLED!  ( r g b position -- )
    NB_LEDS 1- min          \ sécurité anti-débordement
    RGB_struct * LEDS +     \ calcul adresse réelle
    RGB!
  ;

also rmt

\ transmit LEDs stored in LEDS array
: transmitLEDS ( -- )
    channelHandle 0= if
        initWS2812
        channelHandle rmt_disable drop      \ 1. On s'assure que le canal est propre
        channelHandle rmt_enable drop       \ --- Étape 2 : Activation ---
    then
    channelHandle encoderHandle 
    LEDS NB_LEDS RGB_struct * TRANSMIT_CONFIG rmt_transmit
    ?dup if
        ." Erreur transmission LEDs" cr
    then
  ;

only FORTH

<EOF>

