import Cocoa


let fullName : String = ";+;*;(;);i;#"
let fullNameArr : [String] = fullName.components(separatedBy: ";")

for x in fullNameArr {
    if x.count == 0
    {
        print("van ilyen is")
    }
    print(x)
}

struct Values: Hashable {
    var x : String;
    var y : String;
    
    init(x: String, y: String){
        self.x = x;
        self.y = y;
    }
}

var dict: [Values: String] = [:];

dict[Values(x: "a", y: "b")] = "C";
print(dict[Values(x: "a", y: "b")] ?? "error" ) 


