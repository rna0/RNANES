## WELCOME TO RNA NES EMU Main Page

-This emulator is experimental and not a fully working NES emulator written in C#.
-The emulatin is Slow but playable and runs on 30 fps.
-Supported mappers:
  -NromMapper
  -Mmc3Mapper
  
-you can play with a keyboard or this specific [controller](https://www.amazon.com/DOORGA-Controller-Joystick-Raspberry-Emulators/dp/B07W19W65Z/ref=sr_1_6?keywords=NES+usb&qid=1580800839&sr=8-6)

--The reason why this emulator is experimental is that it's backend and frontend are devided into a server and a client respectively.

--This way Clients are the ones who should get the roms and the server should only care about the emulation part.
-The server supports multiple client connections but only the first client to connect will gain access to play the Game.
-All connections after the first will be for viewing only.

things to do:
- Make audio work.
- Add support for More Mappers
- Improve Controller Bindings
- Improve User Interface
- Add seacond controller support
