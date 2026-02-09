\ *************************************
\ tests for ESP32 Fastled
\    Filename:      tests.fs
\    Date:          06 feb 2026
\    Updated:       08 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/tests.fs

\ Créer un espace mémoire pour la config
create &my-rmt-config
&my-rmt-config initRmtConfiguration

also rmt
: setupRmtStructure ( addr -- )
    >r
    RMT_MODE_TX r@ rmtSetMode
    RMT_CHANNEL r@ rmtSetChannel
    RMT_GPIO    r@ rmtSetGpio
              1 r@ rmtSetClkDiv
              1 r@ rmtSetMemBlockNum
\           38000 r@ rmtSetCarrierFreq
    r> drop
    RMT_CHANNEL 1       rmt_set_clk_div drop
\     RMT_CHANNEL 1       rmt_set_mem_block_num drop
\     RMT_CHANNEL 0       rmt_set_tx_loop_mode drop
    RMT_CHANNEL 0 1 1 0 rmt_set_tx_carrier drop
\     config.tx_config.idle_output_en = true;
\     config.tx_config.idle_level = RMT_IDLE_LEVEL_LOW;
  ;

    


\ rmt_config(&config);
&my-rmt-config setupRmtStructure
&my-rmt-config rmt_config

\ rmt_driver_install(config.channel, 0, 0);

only FORTH

&my-rmt-config _RMT_CONFIG dump
hex &my-rmt-config . decimal

<EOF>

