mono wasm-sdk/packager.exe bin/Debug/netstandard2.0/WasmRepo.dll --out=publish --debug --debugrt --search-path=wasm-sdk/framework    --search-path=wasm-sdk/wasm-bcl/wasm/Facades/     --search-path=wasm-sdk/wasm-bcl/wasm --search-path=packages/FSharp.Core/lib/netstandard2.0 --search-path=packages/Bolero.FSharp.Compiler.Service/lib/netstandard2.0 --search-path=packages/System.Reflection.Metadata/lib/netstandard2.0 --search-path=packages/System.Collections.Immutable/lib/netstandard2.0 --aot --emscripten-sdkdir=/mnt/d/Abe/CIPHERWorkspace/mono/sdks/builds/toolchains/emsdk --builddir=buildAOT --mono-sdkdir=/mnt/d/Abe/CIPHERWorkspace/Repos/WasmRepo/wasm-sdk --copy=ifnewer --pinvoke-libs=all #--profile=log --enable-fs