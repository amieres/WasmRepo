# WasmRepo
 Repo to test Wasm


To run the first time:

download WASM SDK: 

    https://jenkins.mono-project.com/job/test-mono-mainline-wasm/label=ubuntu-1804-amd64/4859/Azure/processDownloadRequest/4859/ubuntu-1804-amd64/sdks/wasm/mono-wasm-3a08f494a90.zip

unzip into folder: `wasm-sdk`

Compile WasmRepo.fs:

dotnet build

Publish application:

    package

Run Web Server with:

    cd publish
    python ../wasm-sdk/server.py

open in a browser: localhost/sample.html

Wait for it to load...

Use buttons to Compile & Run