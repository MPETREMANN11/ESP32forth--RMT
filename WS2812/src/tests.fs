\ *************************************
\ tests for ESP32 Fastled
\    Filename:      tests.fs
\    Date:          06 feb 2026
\    Updated:       10 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/tests.fs

initWS2812


<EOF>


: .config  ( conf -- )
    >r
    ." ->gpio_num          " r@ ->gpio_num @ . cr
    ." ->clk_src           " r@ ->clk_src @ . cr
    ." ->resolution_hz     " r@ ->resolution_hz @ . cr
    ." ->mem_block_symbols " r@ ->mem_block_symbols @ . cr
    ." ->trans_queue_depth " r@ ->trans_queue_depth @ . cr
    ." ->intr_priority     " r@ ->intr_priority @ . cr
    ." ->flags_packed      " r@ ->flags_packed @ . cr
    cr ." tx-config : " tx-config hex . decimal cr
    cr r@ 128 dump
    rdrop
  ;







