# ESP32forth RMT 2.0
## Leverage RMT 2.0 in FORTH language with ESP32forth for ESP32 boards
<img src="RMTbanner.jpg"/>
<p>The goal of this repositor is to provide you with all the information, along with practical examples, to fully utilize the RMT 2.0 library.</p>

<p>The transition to RMT 2.0 (introduced with the ESP-IDF v5) marks a shift from a "static" driver to a dynamic, object-oriented architecture. 
  Previously, fixed channel numbers (0-7) and manual clock divider registers were manipulated via `rmt_set_clk_div`. Now, everything relies on Handles 
  (`rmt_channel_handle_t`) dynamically allocated by the system, ensuring greater portability between chips (ESP32, S3, C3).</p>
<p>Signal handling has also changed radically: instead of simply filling an array of items, Encoders (`RMT Encoder`) are used to translate raw data 
into real-time waveforms. This version also introduces the Sync Manager for seamless synchronization of multiple channels, and native DMA integration
to free up the CPU during massive transfers. In short, RMT 2.0 is more complex to initialize, but much more robust, flexible, and efficient in CPU 
usage for complex protocols.</p>

