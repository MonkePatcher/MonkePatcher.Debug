# MonkePatcher.Debug
MonkePatcher.Debug is a primarily static library that wraps some features in the Android SDK/NDK

## Features (WIP)
  - [ ] ADB Debugging
  - [x] Log Window
  - [ ] Log Stream
  - [x] ADB Shell
  - [ ] Automatic Tombstone NDK Stack

## How To Use
### Installing the Android SDK
The Android SDK is needed for some debugging features, the rest use the Android NDK, which will be installed separately due to its file size.<br>
The SDK can be installed using `MonkePatcher.Debug` like so:
```cs
using MonkePatcher.Debug
...
SDKInstaller.Install() 
// The Install() method will only install the SDK if it is not installed already in "%appdata%/MonkePatcher/Debug/platform-tools/"
