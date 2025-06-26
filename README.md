# eLight
Simple flashlight for windows phone 8.1 (C#, XAML).

## Improvements

The project now initializes the `FlashControl` component asynchronously and uses
modern property change notifications. Temporary video files are cleaned up
using asynchronous APIs during application suspension. The singleton resource
manager also relies on `Lazy<T>` for thread-safe initialization.
