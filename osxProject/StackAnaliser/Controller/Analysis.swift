//
//  Analysis.swift
//  StackAnaliser
//
//  Created by Agco Corp on 2022. 10. 25..
//

import Foundation


class Analysis {
    
    private var rules : [TableKeys: String]
    private var state : Stack<String>
    private var stack : Stack<String>
    private var rule : String = ""
    
    private var stateStringChanges = [String]();
    private var stateValueChanges = [String]();
    
    
    public  func result() -> (String, [String],[String]) {
        return (
            state: self.rule,
            stateStringChanges: self.stateStringChanges,
            stateValueChanges: self.stateValueChanges
        )
    }
    
    
    init(rules: [TableKeys: String], text: String){
        self.rules = rules
        self.state = Stack<String>(items: ["E","#"])
        self.stack = Stack<String>(items:[])
        
        stack.push("#")
        for ch in text.reversed() {
            stack.push(String(ch))
        }
    
    }
    
    
    public func makeAnalysis(){
        
        var needRun = true;
        
          while needRun {
       
            guard let stateVal = state.peek() else { return }
            guard let stackVal = stack.peek() else { return }
            
            rule = self.rules[TableKeys(row: stackVal , column:  stateVal)] ?? "error"
           
            
            if(rule == "elfogad"){
                // tyuhééé
                print("succes ág")
                needRun = false
            } else if rule == "pop" {
                // pop művelet
                // DEBUG
                let popedVal  =  self.stack.pop()
                let popedRule =  self.state.pop()
                
                print("popedVal, \(popedVal!)")
                print("poped, \(popedRule!)")
                self.stateStringChanges.append(self.state.description)
                
            } else if rule == "error" {
                print(" error ág ")
                needRun = false
            } else {
                
                let rulez = rule.split(separator: ",")
                let newRule = rulez[0]
                let valueOfTheRule = rulez[1]
                
               let poped = self.state.pop(); 
               // DEBUG
                print(" state poped \(poped!) ")
                
                for elem in newRule.reversed() {
                    var localElement = String(elem)
         
                    if localElement == "ε" {
                        self.stateStringChanges.append( self.state.description )
                        self.stateValueChanges.append( self.stateValueChanges.joined() +  valueOfTheRule )
                        continue
                    }
                    
                    
                    if localElement == "Ț" {
                        localElement = "T’"
                    } else if localElement == "É" {
                        localElement = "E’"
                    }
                    
                    self.state.push(localElement)
                    self.stateStringChanges.append( self.state.description )
                    self.stateValueChanges.append( self.stateValueChanges.joined() +  valueOfTheRule )
                    
                  
                  }
                
               
            }
            
        }
        
  
        
    }
    
    
}
