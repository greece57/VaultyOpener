const fs = require( 'fs' );
const path = require( 'path' );
const { exit } = require('process');

for (item of fs.readdirSync(".")) {
    if (item[0] === "." || item[0] === "r") {
        continue
    }
    let fileData = fs.readFileSync(item, "hex")
    let result = ""
    for (let i = 16; i < fileData.length; i+=1) {
        result += fileData[i]
    }
    fs.writeFileSync(`${item}.png`, result, "hex")

}