\ *************************************
\ main for ESP32 Fastled
\    Filename:      main.fs
\    Date:          06 feb 2026
\    Updated:       06 feb 2026
\    File Version:  1.0
\    MCU:           ESP32-S3 - ESP32 WROOM
\    Forth:         ESP32forth all versions 7.0.7.21+
\    Copyright:     Marc PETREMANN
\    Author:        Marc PETREMANN
\    GNU General Public License
\ **************************************


RECORDFILE /spiffs/main.fs

internals 140 to line-width forth

DEFINED? --espFastled [if] forget --espFastled  [then]
create --espFastled

include /spiffs/datasAndStructs.fs
\ include /spiffs/config.fs
\ include /spiffs/fastleds.fs
\ include /spiffs/clockLed.fs
\ include /spiffs/assert.fs
\ include /spiffs/tests.fs

<EOF>


