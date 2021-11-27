//
//  ViewController.swift
//  PensoMac
//
//  Created by Joonhee Lee on 2021/11/26.
//

// REFERENCE
// Cursor Position Control : https://developer.apple.com/documentation/coregraphics/1456387-cgwarpmousecursorposition
// Cursor Event Control : https://developer.apple.com/documentation/xctest/xcuicoordinate
// NSCursor : https://developer.apple.com/documentation/appkit/nscursor
// GetLastMouseDelta : https://developer.apple.com/documentation/coregraphics/2437197-cggetlastmousedelta

import Cocoa

class ViewController: NSViewController {

    override func viewDidLoad() {
        super.viewDidLoad()

        movePointer(deltaX: 20, deltaY: 20)

    }

    override var representedObject: Any? {
        didSet {
        // Update the view, if already loaded.
        }
    }
    
    func movePointer(deltaX: CGFloat, deltaY: CGFloat) {
        
        let diffX = deltaX / CGFloat(100)
        var diffY = deltaY / CGFloat(100)
        
        for i in 1...100 {
            //usleep(10000)
            let curLocation = NSEvent.mouseLocation
            diffY *= -1
            // print(curLocation.y) // ?? 왜 지랄이지
            let newLocation = CGPoint(x: curLocation.x + diffX, y: curLocation.y - diffY)
            CGWarpMouseCursorPosition(newLocation)
        }
    }
}

