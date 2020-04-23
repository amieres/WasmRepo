# WasmRepo

To run the first time:

- download WASM SDK: 

    https://jenkins.mono-project.com/job/test-mono-mainline-wasm/label=ubuntu-1804-amd64/4980/Azure/

- unzip into folder: **wasm-sdk**
- Compile WasmRepo.fs: `dotnet build`
- install emscripten sdk: 
```
git clone https://github.com/emscripten-core/emsdk.git
cd emsdk
./emsdk install 1.39.9
./emsdk activate -embedded 1.39.9
```
- Publish application: 
```
    ./packageAOTinterp.sh
    cd buildAOTinterp
    ninja
    cd ..
```
- Run Web Server with:
```
    cd publish
    python ../wasm-sdk/server.py
```
- open in a browser: http://localhost:8000/sample.html
- Wait for it to load...
- Use buttons to execute routines: `dir` and `fibo` work, `what works and what doesn't`  work interpreted but not AOT or AOT-interp
