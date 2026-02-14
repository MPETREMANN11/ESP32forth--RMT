# Driwing WS2812 with FORTH for ESP32


<iframe width="1303" height="733" 
  src="https://www.youtube.com/embed/aCv46673pEc" 
  title="ESP32forth démo courte couronne LED simulant une horloge programmée en FORTH pour carte ESP32" 
  frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen>
</iframe>

<p>The WS2812 is an addressable RGB LED that integrates a red, green, and blue LED with a built-in controller in a single package.</p>
<p>Each LED can be individually controlled using a single-wire digital communication protocol.
Data is sent serially from one LED to the next, allowing long chains with minimal wiring.
The WS2812 uses precise timing to encode color information for each LED.</p>
<p>Once data is received, each LED stores its own color and forwards the remaining data downstream.
Addressable LEDs enable complex lighting effects such as animations, gradients, and patterns.</p>
<p>They are commonly driven by microcontrollers like Arduino, ESP32, or Raspberry Pi.
A stable power supply is essential, as current consumption increases with brightness.
Libraries simplify control by handling timing constraints automatically.</p>
<p>WS2812 LEDs are widely used in decorative lighting, displays, and interactive projects.</p>

