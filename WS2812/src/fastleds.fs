\ *************************************
\ words for ESP32 Fastled
\    Filename:      fastled.fs
\    Date:          06 feb 2026
\    Updated:       08 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/fastleds.fs

\     // RMT configuration
\     rmt_config_t config = {};
\     config.rmt_mode = RMT_MODE_TX;
\     config.channel = rmtChannel;
\     config.gpio_num = (gpio_num_t)DATA_PIN;
\     config.clk_div = 1;  // 80MHz
\     config.mem_block_num = 1;
\     config.tx_config.loop_en = false;
\     config.tx_config.carrier_en = false;
\     config.tx_config.idle_output_en = true;
\     config.tx_config.idle_level = RMT_IDLE_LEVEL_LOW;
\     
\     rmt_config(&config);
\     rmt_driver_install(config.channel, 0, 0);
\     
\     initialized = true;


rmt also

create &config
&config initRmtConfiguration

\ : rmtConfig ( -- )
\     &config rmt_config  ?dup
\     if  ." Erreur RMT: " . abort  then
\   ;

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

