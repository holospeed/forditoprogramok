//
//  Stack.swift
//  StackAnaliser
//
//  Created by Agco Corp on 2022. 10. 25..
//

import Foundation


struct Stack<T: CustomStringConvertible> {
    
    var items = [T]();
    
    mutating func push(_ item: T) {
        self.items.insert(item, at: 0)
    }
    
    mutating func pop() -> T? {
        if self.items.isEmpty {
            return nil
        }
        return self.items.removeFirst()
    }
    
    func peek() -> T? {
        if self.items.isEmpty {
            return nil
        }
        return items.first
    }
    
    var description: String {
        var data: String = ""
        
        for item in self.items {
            data += "\(item)"
        }
        return data;
    }
    
}
