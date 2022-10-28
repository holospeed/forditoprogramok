//
//  ViewController.swift
//  StackAnaliser
//
//  Created by Agco Corp on 2022. 10. 23..
//

import Cocoa

class ViewController: NSViewController {

    let f = FileHandler()
    
    
    @IBOutlet weak var expression: NSTextField!
    @IBOutlet weak var ErrorText: NSTextField!
    @IBOutlet weak var multiLineLabel: NSTextField!
    @IBOutlet weak var resultText: NSTextField!
    @IBOutlet weak var gridLabel: NSTextField!
    
        
    override func viewDidLoad() {
        super.viewDidLoad()
    }

    override var representedObject: Any? {
        didSet {
        }
    }

    @IBAction func openFileBTN(_ sender: NSButton) {
        
        let dialog = NSOpenPanel()
      
        
        dialog.title                   = "Choose a file";
        dialog.showsResizeIndicator    = true
        dialog.showsHiddenFiles        = false
        dialog.canChooseDirectories    = true
        dialog.canCreateDirectories    = true
        dialog.allowsMultipleSelection = false
        // dialog.allowedFileTypes        = ["txt"]
        
        if (dialog.runModal() == NSApplication.ModalResponse.OK) {
            
            let result = dialog.url
            guard let localResult = result
                  else { return }
                
            self.f.readFile(localResult.path)
            
            for item in self.f.TableContent {
                gridLabel.stringValue += "–––––––––––––––––––––––––––––––––––––––––––––––––––––\n"
                let localItem = item.replacingOccurrences(of: ";", with: "\t\t")
                gridLabel.stringValue += localItem + "\n"

            }
            
        
            resultText.stringValue = ""
            multiLineLabel.stringValue = ""
                
            } else {
                // User clicked on "Cancel"
                return
            }
        
    }
    
    
    
    @IBAction func AnaliseBTN(_ sender: NSButton) {
        
        ErrorText.stringValue = ""
        resultText.stringValue = ""
        multiLineLabel.stringValue = ""
        
        do {
            let regex = try NSRegularExpression(pattern: "[0-9]+")

            let expressionForAnalisis = regex.stringByReplacingMatches(
                in: expression.stringValue,
                options: [],
                range: NSMakeRange(0, expression.stringValue.count),
                withTemplate: "i"
            )
            
          
            
            let a = Analysis(rules: f.Content, text: expressionForAnalisis)
                a.makeAnalysis()
                let analysisData = a.result()
            
                
            for item in analysisData.1 {
                multiLineLabel.stringValue += item + "\n"
            }
            
            resultText.stringValue = analysisData.0
            resultText.textColor = analysisData.0 == "error" ? .red : .green
      
   
        }
        catch {
            ErrorText.stringValue = "HIBÁS LÉŰPETT FEL AZ ELEMZÉS KÖZBEN!"
            expression.stringValue = ""
        }
        
   
        
        
    }
}

