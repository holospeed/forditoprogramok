//
//  FileHandler.swift
//  StackAnaliser
//
//  Created by Agco Corp on 2022. 10. 24..
//

import Foundation


class FileHandler {
    
    private var content = [TableKeys: String]()
    private var tableContent = [String]()
    
    public var Content:  [TableKeys: String]  {
        return self.content
    }
    
    public var TableContent: [String] {
        return self.tableContent
    }
    
    public func readFile(_ path: String) {
        let filemanager = FileManager.default;
        
        if filemanager.fileExists(atPath: path) {
            
            var i = 0
            var helper = [String]()
            
            guard let file = freopen(path, "r", stdin)
                else { return }
            
            defer {
                fclose(file)
            }
            
            while let line = readLine() {
                
               self.tableContent.append(line)
               
               let data = line.components(separatedBy: ";")
               
                if i == 0 {
                    for item in data {
                        if item.count > 0 {
                            helper.append(item);
                        }
                    }
                } else {
                    
                    var k = 1;
                    for item in helper {
                        if data[k].count > 0 {
                            
                            let localData = data[k]
                                    .replacingOccurrences(of: "E’", with: "É") // replace to one character
                                    .replacingOccurrences(of: "T’", with: "Ț") // replace to one character
                            
                            
                            self.content[TableKeys(row: item, column: data[0] )] = localData
                        }
                        k += 1
                    }
                }
                
                i += 1
            }
            
        } else {
            print("No file found")
        }
     
        
    }
    
    
    
}
