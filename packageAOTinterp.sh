mono wasm-sdk/packager.exe bin/Debug/netstandard2.0/WasmRepo.dll  packages/FSharp.Core/lib/netstandard2.0/FSharp.Core.dll --out=publish --debug --debugrt --search-path=packages/FSharp.Core/lib/netstandard2.0 --search-path=packages/Bolero.FSharp.Compiler.Service/lib/netstandard2.0 --search-path=packages/System.Reflection.Metadata/lib/netstandard2.0 --search-path=packages/System.Collections.Immutable/lib/netstandard2.0 --aot-interp --emscripten-sdkdir=/mnt/d/Abe/CIPHERWorkspace/mono/sdks/builds/toolchains/emsdk --builddir=buildAOTinterp --mono-sdkdir=/mnt/d/Abe/CIPHERWorkspace/Repos/WasmRepo/wasm-sdk --copy=ifnewer --pinvoke-libs=all --aot-assemblies=mscorlib,WasmRepo --no-native-strip --no-linker-exclude-deserialization --no-il-strip #--profile=log --enable-fs