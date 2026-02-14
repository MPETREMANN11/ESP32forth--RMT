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
decimal

 0 constant RMT_MODE_TX     \ for TX mode
 1 constant RMT_MODE_RX     \ for RX mode

ESP32?    [IF]
    \ Sur l'ESP32 original, l'énumération rmt_clock_source_t est très simple :
    0 constant RMT_CLK_SRC_DEFAULT
    1 constant RMT_CLK_SRC_APB          \ (80 MHz)
    2 constant RMT_CLK_SRC_REF_TICK     \ (1 MHz)
[THEN]

ESP32-S3? [IF]    \ soc_periph_rmt_clk_src_t
    0 constant RMT_CLK_SRC_APB       \ Select APB as the source clock
    1 constant RMT_CLK_SRC_RC_FAST   \ Select RC_FAST as the source clock
    2 constant RMT_CLK_SRC_XTAL      \ Select XTAL as the source clock 
    3 constant RMT_CLK_SRC_DEFAULT   \ Select APB as the default choice 
[THEN]

1000 1000 * 10 * value RMT_RES       \ 10 MHz = 100ns par tick

 0 value RMT_CHANNEL     \ choice RMT channel

\ ESP32-S3? [IF]    48 constant SOC_RMT_MEM_WORDS_PER_CHANNEL   [THEN]
\ ESP32?    [IF]    64 constant SOC_RMT_MEM_WORDS_PER_CHANNEL   [THEN]

\ *** Other values *************************************************************

18 constant RMT_GPIO        \ used GPIO

60 constant NB_LEDS         \ defined for ring with 60 LEDs

<EOF>


