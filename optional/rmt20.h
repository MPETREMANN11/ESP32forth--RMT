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


// source: https://docs.espressif.com/projects/arduino-esp32/en/latest/api/rmt.html

// RMT TX: https://github.com/espressif/esp-idf/blob/v5.5.2/components/esp_driver_rmt/include/driver/rmt_tx.h




#define OPTIONAL_RMT_VOCABULARY V(rmt)
#define OPTIONAL_RMT_SUPPORT \
/** rmt_new_tx_channel ( -- handle ) */ \
YV(rmt, rmt_new_tx_channel, \
    rmt_channel_handle_t handle; \
    rmt_tx_channel_config_t* config = (rmt_tx_channel_config_t*)n0; \
    esp_err_t err = rmt_new_tx_channel(config, &handle); \
    n0 = (err == ESP_OK) ? (cell_t)handle : 0; ) \
/** f_rmt_disable ( handle -- ) */  \
YV(rmt, rmt_disable, \
    rmt_disable((rmt_channel_handle_t)n0); DROP ) \
/** f_rmt_enable ( handle -- ) */  \
YV(rmt, rmt_enable, \
    rmt_enable((rmt_channel_handle_t)n0); DROP ) \
/** rmt_transmit ( handle enc_handle payload_ptr bytes config_ptr -- err ) */  \
YV(rmt, rmt_transmit, \
    const rmt_transmit_config_t* config = (const rmt_transmit_config_t*)n0; \
    size_t bytes = (size_t)n1; \
    const void* payload = (const void*)n2; \
    rmt_encoder_handle_t encoder = (rmt_encoder_handle_t)n3; \
    rmt_channel_handle_t channel = (rmt_channel_handle_t)n4; \
    n4 = (cell_t)rmt_transmit(channel, encoder, payload, bytes, config); \
    DROPn(4) \
)