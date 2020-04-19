# WasmRepo

To run the first time:

- download WASM SDK: 

    https://jenkins.mono-project.com/job/test-mono-mainline-wasm/label=ubuntu-1804-amd64/4859/Azure/processDownloadRequest/4859/ubuntu-1804-amd64/sdks/wasm/mono-wasm-3a08f494a90.zip

    and

    https://jenkins.mono-project.com/job/test-mono-mainline-wasm/label=ubuntu-1804-amd64/4859/Azure/processDownloadRequest/4859/ubuntu-1804-amd64/wasm-release-Linux-3a08f494a90ec49d0cb0057f3618e09bd9820f9e.zip

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
- Use buttons to execute routines: `dir` and `fibo` work, `what works and what doesn't`  work interpreted but not AOT
