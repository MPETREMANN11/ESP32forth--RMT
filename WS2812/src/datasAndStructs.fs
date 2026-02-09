\ *************************************
\ datas and structures for RMT 2.0
\    Filename:      datasAndStructs.fs
\    Date:          08 feb 2026
\    Updated:       08 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    RMT:           2.0
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/datasAndStructs.fs

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

\ *** Structures for RMT TX ****************************************************


\ rmt_tx_event_callbacks_t

struct rmt_tx_channel_config_t
    u32 field ->gpio_num            \ GPIO number used by RMT TX channel. Set to -1 if unused
    u32 field ->clk_src             \ Clock source of RMT TX channel
    u32 field ->resolution_hz       \ Channel clock resolution, in Hz 
    u32 field ->mem_block_symbols   \ Size of memory block
    u32 field ->trans_queue_depth   \ Depth of internal transfer queue
    u32 field ->intr_priority
    u32 field ->flags_packed      \ Regroupe with_dma, loop_back, od_mode, etc.

\ Comment remplir flags_packed ?
\ Chaque option occupe un ou plusieurs bits spécifiques dans ce dernier u32. Voici les masques binaires à utiliser :
\     invert_out : bit 0 (valeur 1)
\     with_dma : bit 1 (valeur 2)
\     io_loop_back : bit 2 (valeur 4)
\     io_od_mode : bit 3 (valeur 8)
\     allow_pd : bit 4 (valeur 16)
\     init_level : bit 5 (valeur 32)


\ RMT transmit specific configuration.
struct rmt_transmit_config_t
    i32 field ->loop_count          \ Specify the times of transmission in a loop
                                    \ -1 means transmitting in an infinite loop
    u32 field ->eot_level           \ Set the output level for the "End Of Transmission"
    u32 field ->queue_nonblocking   \ If set, when the transaction queue is full, 
                                    \ driver will not block the thread but return directly
    u32 field ->tx_config_flags     \ Transmit specific config flags


\ Synchronous manager configuration
struct rmt_sync_manager_config_t
    u32 field ->rmt_channel_handle_t \ *tx_channel_array
                                    \ Array of TX channels that are about to be managed by a synchronous controller
    u32 field ->array_size          \ Size of the tx_channel_array
    u32 field ->wait_flags          \ Attendre que tous les canaux soient prêts


only FORTH


<EOF>



