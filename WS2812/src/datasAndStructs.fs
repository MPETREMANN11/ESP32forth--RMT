\ *************************************
\ datas and structures for ESP32 Fastled
\    Filename:      tests.fs
\    Date:          06 feb 2026
\    Updated:       06 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/datasAndStructs.fs

\ *** General constants and values *********************************************

0 constant RMT_MODE_TX      \ for TX mode
1 constant RMT_MODE_RX      \ for RX mode

also structures

\ *** Structure for RGB datas **************************************************

struct RGB          \ define RGB structure
    i8 field ->r
    i8 field ->g
    i8 field ->b

\ get values from RGB address
: RGB@ { addr -- r g b }
    addr ->r c@
    addr ->g c@
    addr ->b c@
  ;

\ store r g b in RGB address
: RGB! { r g b addr -- }
    r addr ->r c!
    g addr ->g c!
    b addr ->b c!
  ;

\ allot a space for n LEDs
: RGBallot ( n -- )
    RGB * allot
  ;

\ *** Structure for RMT config *************************************************

struct RMT_TX_CONFIG
   u32 field ->carrier_freq_hz      \ RMT carrier frequency
   u32 field ->carrier_level        \ Level of the RMT output, when the carrier is applied
   u32 field ->idle_level           \ RMT idle level
    u8 field ->carrier_duty_percent \ RMT carrier duty (%)
    u8 field ->carrier_en           \ RMT carrier enable
    u8 field ->loop_en              \ Enable sending RMT items in a loop
    u8 field ->idle_output_en       \ RMT idle level output enable

struct RMT_RX_CONFIG
   u16 field ->idle_threshold       \ RMT RX idle threshold
    u8 field ->filter_ticks_thresh  \ RMT filter tick number
    u8 field ->filter_en            \ RMT receiver filter enable

\ source: https://docs.espressif.com/projects/esp-idf/en/v4.1/api-reference/peripherals/rmt.html#_CPPv412rmt_config_t
struct RMT_CONFIG
   u32 field ->rmt_mode             \ 0: TX, 1: RX RMT mode: transmitter or receiver
   u32 field ->rmt_channel          \ 0-7 RMT channel
   u32 field ->rmt_gpio_num         \ RMT GPIO number
    u8 field ->rmt_clk_div          \ uint8_t (1-255) RMT channel counter divider
    u8 field ->rmt_mem_block_num    \ uint8_t RMT memory block number
RMT_TX_CONFIG field ->tx_config
RMT_RX_CONFIG field ->rx_config

\ initialize RMT_CONFIG structure
: initRmtConfiguration { addr -- }
    RMT_CONFIG allot
    addr RMT_CONFIG 0 fill
  ;

\ initialize rmt mode
: rmtSetMode ( mode addr -- )
    ->rmt_mode !
  ;

\ initialize channel
: rmtSetChannel ( channel addr -- )
    ->rmt_channel !
  ;

\ initialize gpio
: rmtSetGpio ( gpio addr -- )
    ->rmt_gpio_num !
  ;

only FORTH


<EOF>



