//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

/*
 * ESP32forth RMT v7.0.7.21
 * Revision: 9ae74fa18335b0378a98bd8c693b468cc1265ee5
 */

// New version: 08 feb 2026 - Marc PETREMANN

#ifndef RMT20_H
#define RMT20_H

#include "Arduino.h"
// Dans le Core 3.x, le driver est segmenté.
// On inclut les headers du driver IDF v5 directement :
#include "driver/rmt_tx.h" // Le nouveau driver TX (Version 3.0+)
#include "driver/rmt_rx.h" // Le nouveau driver RX (Version 3.0+)
#include "driver/rmt_encoder.h"
#include "driver/periph_ctrl.h"

class MyRMTDevice {
private:
    uint8_t _pin;
    rmt_channel_handle_t _tx_channel;  // Remplacer RMTTX par rmt_channel_handle_t
    rmt_encoder_handle_t _encoder;     // Handle de l'encodeur (optionnel selon usage)

public:
    MyRMTDevice(uint8_t pin);
    bool begin();
    ~MyRMTDevice();
};

#endif

// Variable globale pour stocker l'état
volatile bool rx_done_flag = false;

// Le vrai callback C++
bool IRAM_ATTR my_on_rx_done(rmt_channel_handle_t ch, const rmt_rx_done_event_data_t *edata, void *user_data) {
    rx_done_flag = true;
    return false; // Retourne true si vous voulez réveiller une tâche FreeRTOS
}

// source: https://docs.espressif.com/projects/arduino-esp32/en/latest/api/rmt.html

// RMT RX/TX: https://github.com/espressif/esp-idf/tree/master/components/esp_driver_rmt/include/driver
//            https://github.com/espressif/arduino-esp32/blob/2cfe8da/docs/en/api/rmt.rst


#define OPTIONAL_RMT_VOCABULARY V(rmt)
#define OPTIONAL_RMT_SUPPORT \
/** rmt_new_rx_channel ( config -- handle|0 ) */ \
YV(rmt, rmt_new_rx_channel, \
    rmt_channel_handle_t handle = NULL; \
    const rmt_rx_channel_config_t* cfg = (const rmt_rx_channel_config_t*)n0; \
    esp_err_t err = rmt_new_rx_channel(cfg, &handle); \
    n0 = (err == ESP_OK) ? (cell_t)handle : 0; NIP ) \
/** rmt_new_tx_channel ( config handle -- err ) */ \
YV(rmt, rmt_new_tx_channel, ({ \
    rmt_channel_handle_t* _ret_chan = (rmt_channel_handle_t*)n0; \
    rmt_tx_channel_config_t* _config = (rmt_tx_channel_config_t*)n1; \
    esp_err_t _err = rmt_new_tx_channel(_config, _ret_chan); \
    n1 = (cell_t)_err; \
    DROP; \
})) \
/** f_rmt_disable ( handle -- err ) */  \
YV(rmt, rmt_disable, \
    rmt_disable((rmt_channel_handle_t)n0); DROP ) \
/** f_rmt_enable ( handle -- err ) */  \
YV(rmt, rmt_enable, \
    rmt_enable((rmt_channel_handle_t)n0); DROP ) \
/** rmt_transmit ( handle encoder payload len config -- err ) */  \
YV(rmt, rmt_transmit, ({ \
    const rmt_transmit_config_t* _cfg = (const rmt_transmit_config_t*)n0; \
    size_t _bytes = (size_t)n1; \
    const void* _payload = (const void*)n2; \
    rmt_encoder_handle_t _enc = (rmt_encoder_handle_t)n3; \
    rmt_channel_handle_t _ch = (rmt_channel_handle_t)n4; \
    n4 = (cell_t)rmt_transmit(_ch, _enc, _payload, _bytes, _cfg); \
    DROPn(4); \
}) ) \
/** rmt.receive ( rx_handle buffer_ptr max_bytes config_ptr -- err ) */ \
YV(rmt, rmt_receive, \
    const rmt_receive_config_t* _rcfg = (const rmt_receive_config_t*)n0; \
    size_t _rbytes = (size_t)n1; \
    void* _rbuf = (void*)n2; \
    rmt_channel_handle_t _rch = (rmt_channel_handle_t)n3; \
    n3 = (cell_t)rmt_receive(_rch, _rbuf, _rbytes, _rcfg); \
    DROPn(3); ) \
/** rmt_apply_carrier ( handle freq duty_percent -- err ) */ \
YV(rmt, rmt_apply_carrier, \
    float duty = (float)n0 / 100.0f; \
    uint32_t freq = (uint32_t)n1; \
    rmt_channel_handle_t ch = (rmt_channel_handle_t)n2; \
    rmt_carrier_config_t carrier_cfg = {}; \
    carrier_cfg.frequency_hz = freq; \
    carrier_cfg.duty_cycle = duty; \
    carrier_cfg.flags.polarity_active_low = false; \
    esp_err_t err = rmt_apply_carrier(ch, &carrier_cfg); \
    n2 = (cell_t)err; \
    DROPn(2) ) \
/** rmt_rx_wait_done ( handle timeout_ms -- err ) */ \
YV(rmt, rmt_rx_wait_done, \
    int timeout = (int)n0; \
    rmt_channel_handle_t ch = (rmt_channel_handle_t)n1; \
    (void)ch; /* Marquer comme intentionnellement inutilisée */ \
    unsigned long start_time = millis(); \
    while ((millis() - start_time) < timeout) { \
        /** Ici on pourrait vérifier un drapeau mis à jour par un callback */ \
        /** Pour l'instant, c'est le point où l'architecture demande un "Event Callback" */ \
    } \
    n1 = (cell_t)ESP_OK; NIP ) \
/** rmt_delete ( handle -- err ) */ \
YV(rmt, rmt_delete, ({ \
    rmt_channel_handle_t _ch = (rmt_channel_handle_t)n0; \
    n0 = (cell_t)rmt_del_channel(_ch); \
}) ) \
/** rmt_del_sync_manager ( sync_handle -- err ) */ \
YV(rmt, rmt_del_sync_manager, ({ \
    rmt_sync_manager_handle_t _sm = (rmt_sync_manager_handle_t)n0; \
    n0 = (cell_t)rmt_del_sync_manager(_sm); \
}) ) \
/** rmt_rx_register_event_callbacks ( sync_handle -- err ) @TODO: vérifier paramètres */ \
YV(rmt, rmt_rx_register_event_callbacks, ({ \
    rmt_rx_event_callbacks_t cbs = { .on_recv_done = my_on_rx_done }; \
    rmt_channel_handle_t _ch = (rmt_channel_handle_t)n0; \
    n0 = (cell_t)rmt_rx_register_event_callbacks(_ch, &cbs, NULL); \
}) ) \
/** rmt_sync_reset ( sync_handle -- err ) */ \
YV(rmt, rmt_sync_reset, ({ \
    rmt_sync_manager_handle_t _sm = (rmt_sync_manager_handle_t)n0; \
    n0 = (cell_t)rmt_sync_reset(_sm); \
}) ) \
/** rmt_new_ws2812_encoder ( resolution_hz -- encoder|0 ) */ \
YV(rmt, rmt_new_ws2812_encoder, \
    uint32_t res = (uint32_t)n0; \
    rmt_encoder_handle_t _enc = NULL; \
    rmt_bytes_encoder_config_t _config = {}; \
    uint32_t t0h = (res / 1000000) * 0.4; \
    uint32_t t0l = (res / 1000000) * 0.85; \
    uint32_t t1h = (res / 1000000) * 0.8; \
    uint32_t t1l = (res / 1000000) * 0.45; \
    _config.bit0.duration0 = t0h; \
    _config.bit0.duration1 = t0l; \
    _config.bit0.level0 = 1; \
    _config.bit0.level1 = 0; \
    _config.bit1.duration0 = t1h; \
    _config.bit1.duration1 = t1l; \
    _config.bit1.level0 = 1; \
    _config.bit1.level1 = 0; \
    _config.flags.msb_first = 1; \
    esp_err_t err = rmt_new_bytes_encoder(&_config, &_enc); \
    n0 = (err == ESP_OK) ? (cell_t)_enc : 0; ) \
/** prepare_ws2812_confi ( gpio -- handle|0 ) */ \
YV(rmt, prepare_ws2812_config, \
    gpio_num_t pin = (gpio_num_t)n0; \
    rmt_channel_handle_t handle = NULL; \
    rmt_tx_channel_config_t tx_cfg = {}; \
    tx_cfg.clk_src = (rmt_clock_source_t)RMT_CLK_SRC_DEFAULT; \
    tx_cfg.gpio_num = pin; \
    tx_cfg.mem_block_symbols = 64; \
    tx_cfg.resolution_hz = 1000000; \
    tx_cfg.trans_queue_depth = 4; \
    tx_cfg.flags.with_dma = 0; \
    esp_err_t err = rmt_new_tx_channel(&tx_cfg, &handle); \
    n0 = ((err == ESP_OK ? (cell_t)handle : 0)) )



