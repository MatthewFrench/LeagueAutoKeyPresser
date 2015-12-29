//
//  AppDelegate.h
//  Key Press Test
//
//  Created by Matthew French on 4/4/15.
//  Copyright (c) 2015 Matthew French. All rights reserved.
//

#import <Cocoa/Cocoa.h>

#include <assert.h>
#include <CoreServices/CoreServices.h>
#include <mach/mach.h>
#include <mach/mach_time.h>
#include <unistd.h>

@interface AppDelegate : NSObject <NSApplicationDelegate> {
    IBOutlet NSComboBox* wardHopKeyComboBox;
    IBOutlet NSComboBox* activeKeyComboBox;
    IBOutlet NSTextField* pressSpell1IntervalText;
    IBOutlet NSTextField* pressSpell2IntervalText;
    IBOutlet NSTextField* pressSpell3IntervalText;
    IBOutlet NSTextField* pressSpell4IntervalText;
    IBOutlet NSTextField* pressActivesIntervalText;
    IBOutlet NSButton* runningCheckBox;
    IBOutlet NSButton* active1OnCheckBox;
    IBOutlet NSButton* active2OnCheckBox;
    IBOutlet NSButton* active3OnCheckBox;
    IBOutlet NSButton* active5OnCheckBox;
    IBOutlet NSButton* active6OnCheckBox;
    IBOutlet NSButton* active7OnCheckBox;
    IBOutlet NSButton* activeWardOnCheckBox;
    IBOutlet NSButton* wardHopOnCheckBox;
    IBOutlet NSButton* qPreactivateWCheckBox;
    IBOutlet NSButton* qPreactivateECheckBox;
    IBOutlet NSButton* qPreactivateRCheckBox;
    IBOutlet NSButton* wPreactivateQCheckBox;
    IBOutlet NSButton* wPreactivateECheckBox;
    IBOutlet NSButton* wPreactivateRCheckBox;
    IBOutlet NSButton* ePreactivateQCheckBox;
    IBOutlet NSButton* ePreactivateWCheckBox;
    IBOutlet NSButton* ePreactivateRCheckBox;
    IBOutlet NSButton* rPreactivateQCheckBox;
    IBOutlet NSButton* rPreactivateWCheckBox;
    IBOutlet NSButton* rPreactivateECheckBox;
    
    IBOutlet NSTextField *qCountLbl, *qSimReleaseLbl, *qReleaseLbl,
    *wCountLbl, *wSimReleaseLbl, *wReleaseLbl,
    *eCountLbl, *eSimReleaseLbl, *eReleaseLbl,
    *rCountLbl, *rSimReleaseLbl, *rReleaseLbl,
    *qStatusLbl, *wStatusLbl, *eStatusLbl, *rStatusLbl;
    
    id activity;
    
    CGPoint mouseLocation;

}
- (void) pressMouseRight:(int)x y:(int)y;
- (void) releaseMouseRight:(int)x y:(int)y;
- (IBAction)turnOnOff:(NSButton*)sender;
- (IBAction)active1OnOff:(id)sender;
- (IBAction)active2OnOff:(id)sender;
- (IBAction)active3OnOff:(id)sender;
- (IBAction)active5OnOff:(id)sender;
- (IBAction)active6OnOff:(id)sender;
- (IBAction)active7OnOff:(id)sender;
- (IBAction)activeWardOnOff:(id)sender;

- (IBAction)qPreactivateW:(id)sender;
- (IBAction)qPreactivateE:(id)sender;
- (IBAction)qPreactivateR:(id)sender;

- (IBAction)wPreactivateQ:(id)sender;
- (IBAction)wPreactivateE:(id)sender;
- (IBAction)wPreactivateR:(id)sender;

- (IBAction)ePreactivateQ:(id)sender;
- (IBAction)ePreactivateW:(id)sender;
- (IBAction)ePreactivateR:(id)sender;

- (IBAction)rPreactivateQ:(id)sender;
- (IBAction)rPreactivateW:(id)sender;
- (IBAction)rPreactivateE:(id)sender;

- (IBAction)wardHopOnOff:(id)sender;
- (IBAction)wardHopKeyChanged:(NSComboBox*)sender;

- (IBAction)activeKeyChanged:(NSComboBox*)sender;

int getTimeInMilliseconds(int64_t absoluteTime);

@end

