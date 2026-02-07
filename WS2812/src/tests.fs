\ *************************************
\ tests for ESP32 Fastled
\    Filename:      tests.fs
\    Date:          06 feb 2026
\    Updated:       07 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/tests.fs

\ 1. Créer un espace mémoire pour la config
create &my-rmt-config
&my-rmt-config initRmtConfiguration

: setupRmtStructure ( addr -- )
    >r
    1 r@ rmtSetMode
    2 r@ rmtSetChannel
   22 r@ rmtSetGpio
\     1                &config rmt_clk_div c!
\     1                &config rmt_mem_block_num c!
    r> drop
;




also rmt
&my-rmt-config rmt_config

&my-rmt-config setupRmtStructure

only FORTH

&my-rmt-config RMT_CONFIG dump
hex &my-rmt-config . decimal

<EOF>


\ 2. Remplir les champs
\ RMT_MODE_TX      ma-config rmt_mode !
\ 0                ma-config rmt_channel !
\ 18               ma-config rmt_gpio_num !
\ 80               ma-config rmt_clk_div c!   \ 1 tick = 1 microseconde
\ 1                ma-config rmt_mem_block_num c!




