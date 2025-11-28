//
//  AppDelegate.m
//  Key Press Test
//
//  Created by Matthew French on 4/4/15.
//  Copyright (c) 2015 Matthew French. All rights reserved.
//

#import "AppDelegate.h"
#import <ApplicationServices/ApplicationServices.h>

@interface AppDelegate ()

@property (weak) IBOutlet NSWindow *window;
@end

@implementation AppDelegate

//NSTimer* timer = NULL;

bool keyQPressed = false;
bool keyWPressed = false;
bool keyEPressed = false;
bool keyRPressed = false;
bool keyTPressed = false;

bool pressingSpell1 = false;
bool pressingSpell2 = false;
bool pressingSpell3 = false;
bool pressingSpell4 = false;

uint64_t pressingSpell1LastTime = 0;
uint64_t pressingSpell2LastTime = 0;
uint64_t pressingSpell3LastTime = 0;
uint64_t pressingSpell4LastTime = 0;

uint64_t pressedDLastTime = 0;
uint64_t pressedFLastTime = 0;

uint64_t pressingActivesLastTime = 0;

uint64_t pressingWardLastTime = 0;

uint64_t wardHopLastTime = 0;

double pressSpell1Interval = 10;
double pressSpell2Interval = 10;
double pressSpell3Interval = 10;
double pressSpell4Interval = 100;
double pressActivesInterval = 100;

bool running = true;
bool active1On = false;
bool active2On = false;
bool active3On = false;
bool active5On = false;
bool active6On = false;
bool active7On = false;
bool activeWardOn = false;

bool wardHopOn = false;
bool qPreactivateW = false;
bool qPreactivateE = false;
bool qPreactivateR = false;

bool wPreactivateQ = false;
bool wPreactivateE = false;
bool wPreactivateR = false;

bool ePreactivateQ = false;
bool ePreactivateW = false;
bool ePreactivateR = false;

bool rPreactivateQ = false;
bool rPreactivateW = false;
bool rPreactivateE = false;

char wardHopKey = 'Q';

char activeKey = 'E';

CFMachPortRef      eventTap;

dispatch_source_t timer, uiTimer;
dispatch_source_t CreateDispatchTimer(uint64_t intervalNanoseconds,
                                      uint64_t leewayNanoseconds,
                                      dispatch_queue_t queue,
                                      dispatch_block_t block)
{
    dispatch_source_t timer = dispatch_source_create(DISPATCH_SOURCE_TYPE_TIMER,
                                                     0, 0, queue);
    if (timer)
    {
        dispatch_source_set_timer(timer, dispatch_walltime(NULL, 0), intervalNanoseconds, leewayNanoseconds);
        dispatch_source_set_event_handler(timer, block);
        dispatch_resume(timer);
    }
    return timer;
}

- (void)applicationDidFinishLaunching:(NSNotification *)aNotification {
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    if ([defaults boolForKey:@"saved"] == YES) {
        wardHopKey = [defaults integerForKey:@"wardHopKey"];
        activeKey = [defaults integerForKey:@"activeKey"];
        pressSpell1Interval = [defaults doubleForKey:@"pressSpell1Interval"];
        pressSpell2Interval = [defaults doubleForKey:@"pressSpell2Interval"];
        pressSpell3Interval = [defaults doubleForKey:@"pressSpell3Interval"];
        pressSpell4Interval = [defaults doubleForKey:@"pressSpell4Interval"];
        pressActivesInterval = [defaults doubleForKey:@"pressActivesInterval"];
        running = [defaults boolForKey:@"running"];
        active1On = [defaults boolForKey:@"active1On"];
        active2On = [defaults boolForKey:@"active2On"];
        active3On = [defaults boolForKey:@"active3On"];
        active5On = [defaults boolForKey:@"active5On"];
        active6On = [defaults boolForKey:@"active6On"];
        active7On = [defaults boolForKey:@"active7On"];
        activeWardOn = [defaults boolForKey:@"activeWardOn"];
        wardHopOn = [defaults boolForKey:@"wardHopOn"];
        qPreactivateW = [defaults boolForKey:@"qPreactivateW"];
        qPreactivateE = [defaults boolForKey:@"qPreactivateE"];
        qPreactivateR = [defaults boolForKey:@"qPreactivateR"];
        wPreactivateQ = [defaults boolForKey:@"wPreactivateQ"];
        wPreactivateE = [defaults boolForKey:@"wPreactivateE"];
        wPreactivateR = [defaults boolForKey:@"wPreactivateR"];
        ePreactivateQ = [defaults boolForKey:@"ePreactivateQ"];
        ePreactivateW = [defaults boolForKey:@"ePreactivateW"];
        ePreactivateR = [defaults boolForKey:@"ePreactivateR"];
        rPreactivateQ = [defaults boolForKey:@"rPreactivateQ"];
        rPreactivateW = [defaults boolForKey:@"rPreactivateW"];
        rPreactivateE = [defaults boolForKey:@"rPreactivateE"];
    }
    [wardHopKeyComboBox setStringValue:[NSString stringWithFormat:@"%c",wardHopKey]];
    [activeKeyComboBox setStringValue:[NSString stringWithFormat:@"%c",activeKey]];
    [pressSpell1IntervalText setIntegerValue:pressSpell1Interval];
    [pressSpell2IntervalText setIntegerValue:pressSpell2Interval];
    [pressSpell3IntervalText setIntegerValue:pressSpell3Interval];
    [pressSpell4IntervalText setIntegerValue:pressSpell4Interval];
    [pressActivesIntervalText setIntegerValue:pressActivesInterval];
    [runningCheckBox setState:running];
    [active1OnCheckBox setState:active1On];
    [active2OnCheckBox setState:active2On];
    [active3OnCheckBox setState:active3On];
    [active5OnCheckBox setState:active5On];
    [active6OnCheckBox setState:active6On];
    [active7OnCheckBox setState:active7On];
    [activeWardOnCheckBox setState:activeWardOn];
    [wardHopOnCheckBox setState:wardHopOn];
    [qPreactivateWCheckBox setState:qPreactivateW];
    [qPreactivateECheckBox setState:qPreactivateE];
    [qPreactivateRCheckBox setState:qPreactivateR];
    [wPreactivateQCheckBox setState:wPreactivateQ];
    [wPreactivateECheckBox setState:wPreactivateE];
    [wPreactivateRCheckBox setState:wPreactivateR];
    [ePreactivateQCheckBox setState:ePreactivateQ];
    [ePreactivateWCheckBox setState:ePreactivateW];
    [ePreactivateRCheckBox setState:ePreactivateR];
    [rPreactivateQCheckBox setState:rPreactivateQ];
    [rPreactivateWCheckBox setState:rPreactivateW];
    [rPreactivateECheckBox setState:rPreactivateE];
    
    //retrieving
    //[defaults boolForKey:@"testBool"];
    [self.window setRestorable:true];
    
    
    
    
    // Insert code here to initialize your application
    globalSelf = self;
    [self createTap];
    [self createTapMouse];
    
    pressingSpell1LastTime = mach_absolute_time();
    pressingSpell2LastTime = mach_absolute_time();
    pressingSpell3LastTime = mach_absolute_time();
    pressingSpell4LastTime = mach_absolute_time();
    pressingActivesLastTime = mach_absolute_time();
    
    [self.window makeKeyAndOrderFront:self];
    [self.window setOrderedIndex:0];
    [NSApp activateIgnoringOtherApps:YES];
    
    
    [[NSProcessInfo processInfo] disableAutomaticTermination:@"Good Reason"];
    
    if ([[NSProcessInfo processInfo] respondsToSelector:@selector(beginActivityWithOptions:reason:)]) {
        self->activity = [[NSProcessInfo processInfo] beginActivityWithOptions:0x00FFFFFF reason:@"receiving messages"];
    }
    /*
     [[NSProcessInfo processInfo] beginActivityWithOptions:NSActivityIdleSystemSleepDisabled| NSActivitySuddenTerminationDisabled reason:@"Good Reason 2"];
     
     [[NSProcessInfo processInfo] beginActivityWithOptions:NSActivityUserInitiated | NSActivityLatencyCritical reason:@"Good Reason 3"];
     */
    //timer = [NSTimer scheduledTimerWithTimeInterval:1.0/1000.0
    //                                         target:self
    //                                       selector:@selector(timerLogic)
    //                                       userInfo:nil
    //                                        repeats:YES];
    timer = CreateDispatchTimer(NSEC_PER_SEC/1000, //30ull * NSEC_PER_SEC
                                0, //1ull * NSEC_PER_SEC
                                dispatch_get_main_queue(),
                                ^{ [self timerLogic]; });
    
    uiTimer = CreateDispatchTimer(NSEC_PER_SEC/1000, //30ull * NSEC_PER_SEC
                                  0, //1ull * NSEC_PER_SEC
                                  dispatch_get_main_queue(),
                                  ^{ [self uiLogic]; });
    
    for (NSRunningApplication *app in
           [[NSWorkspace sharedWorkspace] runningApplications]) {
        
        NSLog(@"Running app %@", [app localizedName]);
      }
}
- (IBAction)turnOnOff:(NSButton*)sender {
    keyQPressed = 0;
    keyWPressed = 0;
    keyEPressed = 0;
    keyRPressed = 0;
    
    pressingSpell1 = false;
    pressingSpell2 = false;
    pressingSpell3 = false;
    pressingSpell4 = false;
    
    pressingSpell1LastTime = 0;
    pressingSpell2LastTime = 0;
    pressingSpell3LastTime = 0;
    pressingSpell4LastTime = 0;
    
    pressingActivesLastTime = 0;
    running = !running;
    
    countTKey = 0;
    qCount = 0;
    wCount = 0;
    eCount = 0;
    rCount = 0;
    
    [self removeFocus];
}
bool lastPressedQ = false;
int lastQCount = 0, lastQSimRelease = 0, lastQRelease = 0;
bool lastPressedW = false;
int lastWCount = 0, lastWSimRelease = 0, lastWRelease = 0;
bool lastPressedE = false;
int lastECount = 0, lastESimRelease = 0, lastERelease = 0;
bool lastPressedR = false;
int lastRCount = 0, lastRSimRelease = 0, lastRRelease = 0;
- (void)uiLogic {
    //Q
    if (pressingSpell1 && !lastPressedQ) {
        [qStatusLbl setTextColor:[NSColor greenColor]];
        [qStatusLbl setStringValue:@"On"];
        lastPressedQ = true;
    } else if (lastPressedQ && !pressingSpell1) {
        [qStatusLbl setTextColor:[NSColor redColor]];
        [qStatusLbl setStringValue:@"Off"];
        lastPressedQ = false;
    }
    if (lastQCount != qCount) {
        lastQCount = qCount;
        [qCountLbl setStringValue:[NSString stringWithFormat:@"%d", qCount]];
    }
    if (lastQSimRelease != qCountSimulatedReleases) {
        lastQSimRelease = qCountSimulatedReleases;
        [qSimReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", qCountSimulatedReleases]];
    }
    if (lastQRelease != qCountReleases) {
        lastQRelease = qCountReleases;
        [qReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", qCountReleases]];
    }
    
    //W
    if (pressingSpell2 && !lastPressedW) {
        [wStatusLbl setTextColor:[NSColor greenColor]];
        [wStatusLbl setStringValue:@"On"];
        lastPressedW = true;
    } else if (lastPressedW && !pressingSpell2) {
        [wStatusLbl setTextColor:[NSColor redColor]];
        [wStatusLbl setStringValue:@"Off"];
        lastPressedW = false;
    }
    if (lastWCount != wCount) {
        lastWCount = wCount;
        [wCountLbl setStringValue:[NSString stringWithFormat:@"%d", wCount]];
    }
    if (lastWSimRelease != wCountSimulatedReleases) {
        lastWSimRelease = wCountSimulatedReleases;
        [wSimReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", wCountSimulatedReleases]];
    }
    if (lastWRelease != wCountReleases) {
        lastWRelease = wCountReleases;
        [wReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", wCountReleases]];
    }
    
    //e
    if (pressingSpell3 && !lastPressedE) {
        [eStatusLbl setTextColor:[NSColor greenColor]];
        [eStatusLbl setStringValue:@"On"];
        lastPressedE = true;
    } else if (lastPressedE && !pressingSpell3) {
        [eStatusLbl setTextColor:[NSColor redColor]];
        [eStatusLbl setStringValue:@"Off"];
        lastPressedE = false;
    }
    if (lastECount != eCount) {
        lastECount = eCount;
        [eCountLbl setStringValue:[NSString stringWithFormat:@"%d", eCount]];
    }
    if (lastESimRelease != eCountSimulatedReleases) {
        lastESimRelease = eCountSimulatedReleases;
        [eSimReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", eCountSimulatedReleases]];
    }
    if (lastERelease != eCountReleases) {
        lastERelease = eCountReleases;
        [eReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", eCountReleases]];
    }
    
    //r
    if (pressingSpell4 && !lastPressedR) {
        [rStatusLbl setTextColor:[NSColor greenColor]];
        [rStatusLbl setStringValue:@"On"];
        lastPressedR = true;
    } else if (lastPressedR && !pressingSpell4) {
        [rStatusLbl setTextColor:[NSColor redColor]];
        [rStatusLbl setStringValue:@"Off"];
        lastPressedR = false;
    }
    if (lastRCount != rCount) {
        lastRCount = rCount;
        [rCountLbl setStringValue:[NSString stringWithFormat:@"%d", rCount]];
    }
    if (lastRSimRelease != rCountSimulatedReleases) {
        lastRSimRelease = rCountSimulatedReleases;
        [rSimReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", rCountSimulatedReleases]];
    }
    if (lastRRelease != rCountReleases) {
        lastRRelease = rCountReleases;
        [rReleaseLbl setStringValue:[NSString stringWithFormat:@"%d", rCountReleases]];
    }
}
- (IBAction)active1OnOff:(id)sender {
    active1On = !active1On;
    [self removeFocus];
}
- (IBAction)active2OnOff:(id)sender {
    active2On = !active2On;
    [self removeFocus];
}
- (IBAction)active3OnOff:(id)sender {
    active3On = !active3On;
    [self removeFocus];
}
- (IBAction)active5OnOff:(id)sender {
    active5On = !active5On;
    [self removeFocus];
}
- (IBAction)active6OnOff:(id)sender {
    active6On = !active6On;
    [self removeFocus];
}
- (IBAction)active7OnOff:(id)sender {
    active7On = !active7On;
    [self removeFocus];
}
- (IBAction)activeWardOnOff:(id)sender {
    activeWardOn = !activeWardOn;
    [self removeFocus];
}

- (void)removeFocus {
    [[self window] makeFirstResponder:nil];
}
- (void)controlTextDidChange:(NSNotification *)aNotification {
    NSTextField* sender = [aNotification object];
    if (sender == pressSpell1IntervalText) {
        if ([[sender stringValue] length] == 0) {
            pressSpell1Interval = 0;
        } else {
            pressSpell1Interval = [sender doubleValue];
        }
    }
    if (sender == pressSpell2IntervalText) {
        if ([[sender stringValue] length] == 0) {
            pressSpell2Interval = 0;
        } else {
            pressSpell2Interval = [sender doubleValue];
        }
    }
    if (sender == pressSpell3IntervalText) {
        if ([[sender stringValue] length] == 0) {
            pressSpell3Interval = 0;
        } else {
            pressSpell3Interval = [sender doubleValue];
        }
    }
    if (sender == pressSpell4IntervalText) {
        if ([[sender stringValue] length] == 0) {
            pressSpell4Interval = 0;
        } else {
            pressSpell4Interval = [sender doubleValue];
        }
    }
    if (sender == pressActivesIntervalText) {
        if ([[sender stringValue] length] == 0) {
            pressActivesInterval = 0;
        } else {
            pressActivesInterval = [sender doubleValue];
        }
    }
}
- (IBAction)activeKeyChanged:(NSComboBox*)sender {
    activeKey = [[sender stringValue] characterAtIndex:0];
    [self removeFocus];
}

- (void)applicationWillTerminate:(NSNotification *)aNotification {
    // Insert code here to tear down your application
    //saving
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    [defaults setInteger:YES forKey:@"saved"];
    [defaults setInteger:wardHopKey forKey:@"wardHopKey"];
    [defaults setInteger:activeKey forKey:@"activeKey"];
    [defaults setDouble:pressSpell1Interval forKey:@"pressSpell1Interval"];
    [defaults setDouble:pressSpell2Interval forKey:@"pressSpell2Interval"];
    [defaults setDouble:pressSpell3Interval forKey:@"pressSpell3Interval"];
    [defaults setDouble:pressSpell4Interval forKey:@"pressSpell4Interval"];
    [defaults setDouble:pressActivesInterval forKey:@"pressActivesInterval"];
    [defaults setBool:running forKey:@"running"];
    [defaults setBool:active1On forKey:@"active1On"];
    [defaults setBool:active2On forKey:@"active2On"];
    [defaults setBool:active3On forKey:@"active3On"];
    [defaults setBool:active5On forKey:@"active5On"];
    [defaults setBool:active6On forKey:@"active6On"];
    [defaults setBool:active7On forKey:@"active7On"];
    [defaults setBool:activeWardOn forKey:@"activeWardOn"];
    [defaults setBool:wardHopOn forKey:@"wardHopOn"];
    [defaults setBool:qPreactivateW forKey:@"qPreactivateW"];
    [defaults setBool:qPreactivateE forKey:@"qPreactivateE"];
    [defaults setBool:qPreactivateR forKey:@"qPreactivateR"];
    [defaults setBool:wPreactivateQ forKey:@"wPreactivateQ"];
    [defaults setBool:wPreactivateE forKey:@"wPreactivateE"];
    [defaults setBool:wPreactivateR forKey:@"wPreactivateR"];
    [defaults setBool:ePreactivateQ forKey:@"ePreactivateQ"];
    [defaults setBool:ePreactivateW forKey:@"ePreactivateW"];
    [defaults setBool:ePreactivateR forKey:@"ePreactivateR"];
    [defaults setBool:rPreactivateQ forKey:@"rPreactivateQ"];
    [defaults setBool:rPreactivateW forKey:@"rPreactivateW"];
    [defaults setBool:rPreactivateE forKey:@"rPreactivateE"];
}
static AppDelegate *globalSelf;
int countTKey = 0;

int qCount = 0;
int qCountSimulatedReleases = 0;
int qCountReleases = 0;

int wCount = 0;
int wCountSimulatedReleases = 0;
int wCountReleases = 0;

int eCount = 0;
int eCountSimulatedReleases = 0;
int eCountReleases = 0;

int rCount = 0;
int rCountSimulatedReleases = 0;
int rCountReleases = 0;

CGEventRef myCGEventCallback(CGEventTapProxy proxy, CGEventType type,
                             CGEventRef event, void *refcon)
{
    CGKeyCode keycode = (CGKeyCode)CGEventGetIntegerValueField(event, kCGKeyboardEventKeycode);
    
    int64_t autoRepeat = CGEventGetIntegerValueField(event, kCGKeyboardEventAutorepeat);
    // Todo, determine if this turns of key repeating on all keys
    // Todo, mayber only add behavior here if league is forefront
    if (autoRepeat == 1) { //Don't want repeating keys
        return NULL;
    }
    
    bool returnEvent = true;
    
    bool runLogicImmediately = false;
    
    if (keycode == 17) { //T key
        if (type == kCGEventKeyDown) {
            countTKey += 1;
            if (keyTPressed == false) runLogicImmediately = true;
        } else if (type == kCGEventKeyUp) {
            countTKey -= 1;
        }
        if (countTKey == 0) {
            wardHopLastTime = 0;
            keyTPressed = false;
        } else {
            keyTPressed = true;
        }
    }
    
    if (keycode == 12) { //Q
        if (running) {
        if (type == kCGEventKeyDown) {
            qCount += 1;
            if (pressingSpell1 == false) runLogicImmediately = true;
        } else if (type == kCGEventKeyUp) {
            qCount -= 1;
            qCountReleases += 1;
        }
        if (pressingSpell1 == false && qCount >= 1) {
            pressingSpell1LastTime = 0;
            pressingSpell1 = true;
            qCount = 0;
            qCountReleases = 0;
            qCountSimulatedReleases = 0;
            returnEvent = false;
        }
        if (pressingSpell1 == true && qCount == -1 && qCountReleases + 1 >= qCountSimulatedReleases) {
            returnEvent = false;
            qCount = 0;
            pressingSpell1 = false;
            qCountReleases = 0;
            qCountSimulatedReleases = 0;
        }
        } else {
            if (type == kCGEventKeyDown) {
                runLogicImmediately = true;
                pressingSpell1 = true;
            } else if (type == kCGEventKeyUp) {
                pressingSpell1 = false;
            }
        }
    }
    
    if (keycode == 13) { //w
        if (running) {
        if (type == kCGEventKeyDown) {
            wCount += 1;
            if (pressingSpell2 == false) runLogicImmediately = true;
        } else if (type == kCGEventKeyUp) {
            wCount -= 1;
            wCountReleases += 1;
        }
        if (pressingSpell2 == false && wCount >= 1) {
            pressingSpell2LastTime = 0;
            pressingSpell2 = true;
            wCount = 0;
            wCountReleases = 0;
            wCountSimulatedReleases = 0;
            returnEvent = false;
        }
        if (pressingSpell2 == true && wCount == -1 && wCountReleases + 1 >= wCountSimulatedReleases) {
            returnEvent = false;
            wCount = 0;
            pressingSpell2 = false;
            wCountReleases = 0;
            wCountSimulatedReleases = 0;
        }
        } else {
            if (type == kCGEventKeyDown) {
                runLogicImmediately = true;
                pressingSpell2 = true;
            } else if (type == kCGEventKeyUp) {
                pressingSpell2 = false;
            }
        }
    }
    
    if (keycode == 14) { //e
        if (running) {
        if (type == kCGEventKeyDown) {
            eCount += 1;
            if (pressingSpell3 == false) runLogicImmediately = true;
            //NSLog(@"E down eCount: %d", eCount);
        } else if (type == kCGEventKeyUp) {
            eCount -= 1;
            eCountReleases += 1;
            //NSLog(@"E up eCount: %d, eCountReleases: %d, eCountSimulatedReleases: %d", eCount, eCountReleases, eCountSimulatedReleases);
        }
        if (pressingSpell3 == false && eCount >= 1) {
            pressingSpell3LastTime = 0;
            pressingSpell3 = true;
            eCount = 0;
            eCountReleases = 0;
            eCountSimulatedReleases = 0;
            returnEvent = false;
        }
        if (pressingSpell3 == true && eCount == -1 && eCountReleases + 1 >= eCountSimulatedReleases) {
            returnEvent = false;
            eCount = 0;
            pressingSpell3 = false;
            eCountReleases = 0;
            eCountSimulatedReleases = 0;
        }
        } else {
            if (type == kCGEventKeyDown) {
                runLogicImmediately = true;
                pressingSpell3 = true;
            } else if (type == kCGEventKeyUp) {
                pressingSpell3 = false;
            }
        }
    }
    
    if (keycode == 15) { //r
        if (running) {
        if (type == kCGEventKeyDown) {
            rCount += 1;
            if (pressingSpell4 == false) runLogicImmediately = true;
        } else if (type == kCGEventKeyUp) {
            rCount -= 1;
            rCountReleases += 1;
        }
        if (pressingSpell4 == false && rCount >= 1) {
            pressingSpell4LastTime = 0;
            pressingSpell4 = true;
            rCount = 0;
            rCountReleases = 0;
            rCountSimulatedReleases = 0;
            returnEvent = false;
        }
        if (pressingSpell4 == true && rCount == -1 && rCountReleases + 1 >= rCountSimulatedReleases) {
            returnEvent = false;
            rCount = 0;
            pressingSpell4 = false;
            rCountReleases = 0;
            rCountSimulatedReleases = 0;
        }
        } else {
            if (type == kCGEventKeyDown) {
                runLogicImmediately = true;
                pressingSpell4 = true;
            } else if (type == kCGEventKeyUp) {
                pressingSpell4 = false;
            }
        }
    }
    
    
    
    if (runLogicImmediately  && running) {
        [globalSelf timerLogic];
    }
    if (returnEvent) {
        return event;
    }
    return NULL;
}

CGEventRef myCGEventCallbackMouse(CGEventTapProxy proxy, CGEventType type,
                                  CGEventRef event, void *refcon)
{
    
    globalSelf->mouseLocation = CGEventGetLocation(event);
    
    return event;
}

//uint64_t lastTime;
//int writeEvery = 2000;
//int currentWrite = 0;
//double avg = 0;

- (void)timerLogic {
    // This code is so ugly, I must be bad at writing Objective C
    // This needs serious cleanup and re-write
    
    // Can check the forefront application with this
    //NSWorkspace().frontmostApplication?.bundleIdentifier;
    
    
    /*
    //Profile code? See how fast it's running?
    if (currentWrite >= writeEvery)
    {
        //uint64_t elapsedTime2 = mach_absolute_time() - lastTime;
        //NSLog(@"Elapsed Time: %d milliseconds and mouse location: %f %f",  getTimeInMilliseconds(avg/writeEvery), mouseLocation.x, mouseLocation.y);
        lastTime = mach_absolute_time();
        currentWrite = 0;
        avg = 0;
    }
    else
    {
        currentWrite++;
        avg += mach_absolute_time() - lastTime;
        lastTime = mach_absolute_time();
    }*/
    //Ward hop
    if (keyTPressed && wardHopOn) {
        //Place ward
        uint64_t elapsedTime = mach_absolute_time() - wardHopLastTime;
        if (getTimeInMilliseconds(elapsedTime) >= 1000) {
            wardHopLastTime =mach_absolute_time();
            [self tapWard];
            
            //Try to hop
            for (int i = 0; i <= 4; i++) { //Keep trying for 200 milliseconds
                dispatch_after(dispatch_time(DISPATCH_TIME_NOW, NSEC_PER_SEC/1000 * (i * 50 + 30)), dispatch_get_main_queue(), ^{
                    if (wardHopKey == 'Q') [self tapQ];
                    if (wardHopKey == 'W') [self tapW];
                    if (wardHopKey == 'E') [self tapE];
                    if (wardHopKey == 'R') [self tapR];
                });
            }
        } else {
            if (wardHopKey == 'Q') [self preactivateQ:pressSpell1Interval];
            if (wardHopKey == 'W') [self preactivateW:pressSpell2Interval];
            if (wardHopKey == 'E') [self preactivateE:pressSpell3Interval];
            if (wardHopKey == 'R') [self preactivateR:pressSpell4Interval];
        }
    }
    
    
    
    
        
        
        if (pressingSpell1) {
            if (running) {
            if (qPreactivateW) [self preactivateW:pressSpell1Interval];
            if (qPreactivateE) [self preactivateE:pressSpell1Interval];
            if (qPreactivateR) [self preactivateR:pressSpell1Interval];
            [self preactivateQ:pressSpell1Interval];
            }
            if (activeKey == 'Q') {[self activateActives];}
        }
        if (pressingSpell2) {
            if (running) {
            if (wPreactivateQ) [self preactivateQ:pressSpell2Interval];
            if (wPreactivateE) [self preactivateE:pressSpell2Interval];
            if (wPreactivateR) [self preactivateR:pressSpell2Interval];
            [self preactivateW:pressSpell2Interval];
            }
            if (activeKey == 'W') {[self activateActives];}
        }
        if (pressingSpell3) {
            if (running) {
            if (ePreactivateQ) [self preactivateQ:pressSpell3Interval];
            if (ePreactivateW) [self preactivateW:pressSpell3Interval];
            if (ePreactivateR) [self preactivateR:pressSpell3Interval];
            [self preactivateE:pressSpell3Interval];
            }
            if (activeKey == 'E') {[self activateActives];}
        }
        if (pressingSpell4) {
            if (running) {
            if (rPreactivateQ) [self preactivateQ:pressSpell4Interval];
            if (rPreactivateW) [self preactivateW:pressSpell4Interval];
            if (rPreactivateE) [self preactivateE:pressSpell4Interval];
            [self preactivateR:pressSpell4Interval];
            }
            if (activeKey == 'R') {[self activateActives];}
        }
    
    /* else {
      if (keyTPressed) {
      uint64_t elapsedTime = mach_absolute_time() - wardHopLastTime;
      if (getTimeInMilliseconds( elapsedTime ) >= 550) {
      wardHopLastTime =mach_absolute_time();
      [self tapRecall];
      [self tapW];
      [self pressMouseRight:mouseLocation.x y:mouseLocation.y];
      [self releaseMouseRight:mouseLocation.x y:mouseLocation.y];
      for (int i = 10; i < 550; i+= 5) {
      dispatch_after(dispatch_time(DISPATCH_TIME_NOW, NSEC_PER_SEC/1000.0 * i), dispatch_get_main_queue(), ^{
      [self tapQ];
      //        [self tapQ];
      });
      }
      }
      }
      }*/
}
- (void) preactivateQ:(double)interval {
    uint64_t elapsedTime = mach_absolute_time() - pressingSpell1LastTime;
    if (getTimeInMilliseconds(elapsedTime) >= interval) {
        pressingSpell1LastTime = mach_absolute_time();
        [self tapQ];
    }
}
- (void) preactivateW:(double)interval {
    uint64_t elapsedTime = mach_absolute_time() - pressingSpell2LastTime;
    if (getTimeInMilliseconds(elapsedTime) >= interval) {
        pressingSpell2LastTime = mach_absolute_time();
        [self tapW];
    }
}
- (void) preactivateE:(double)interval {
    uint64_t elapsedTime = mach_absolute_time() - pressingSpell3LastTime;
    if (getTimeInMilliseconds(elapsedTime) >= interval) {
        pressingSpell3LastTime = mach_absolute_time();
        [self tapE];
    }
}
- (void) preactivateR:(double)interval {
    uint64_t elapsedTime = mach_absolute_time() - pressingSpell4LastTime;
    if (getTimeInMilliseconds(elapsedTime) >= interval) {
        pressingSpell4LastTime = mach_absolute_time();
        [self tapR];
    }
}
- (void) activateActives {
    uint64_t elapsedTime = mach_absolute_time() - pressingActivesLastTime;
    if (getTimeInMilliseconds(elapsedTime) >= pressActivesInterval) {
        pressingActivesLastTime = mach_absolute_time();
        if (active1On) {
            [self tapActive1];
        }
        if (active2On) {
            [self tapActive2];
        }
        if (active3On) {
            [self tapActive3];
        }
        if (active5On) {
            [self tapActive5];
        }
        if (active6On) {
            [self tapActive6];
        }
        if (active7On) {
            [self tapActive7];
        }
    }
    if (activeWardOn) {
        uint64_t elapsedTime = mach_absolute_time() - pressingWardLastTime;
        if (getTimeInMilliseconds(elapsedTime) >= 6000) {
            pressingWardLastTime = mach_absolute_time();
            [self tapWard];
        }
    }
}
/*
 - (void)runLogicPress {
 if (countQKeyDown > 0) {
 pressingSpell1 = true;
 if (countQKeyDown > 1) {
 NSLog(@"Releasing Q");
 [self releaseQ];
 }
 }
 if (countQKeyUp == 1) {
 //Q Key went up so reset everything
 pressingSpell1LastTime = 0;
 pressingSpell1 = false;
 countQKeyDown = 0;
 countQKeyUp = 0;
 //[self releaseQ];
 }
 
 if (countEKeyDown > 0) {
 //E Key went down so reset everything
 pressingSpell3 = false;
 countEKeyDown = 0;
 countEKeyUp = 0;
 } else if (countEKeyUp == 1) {
 pressingSpell3LastTime = 0;
 pressingSpell3 = true;
 [self releaseE];
 }
 
 if (countWKeyDown > 0) {
 //W Key went down so reset everything
 pressingSpell2 = false;
 countWKeyDown = 0;
 countWKeyUp = 0;
 } else if (countWKeyUp == 1) {
 pressingSpell2LastTime = 0;
 pressingSpell2 = true;
 [self releaseW];
 }
 
 if (countRKeyDown > 0) {
 //W Key went down so reset everything
 pressingSpell4 = false;
 countRKeyDown = 0;
 countRKeyUp = 0;
 } else if (countRKeyUp == 1) {
 pressingSpell4LastTime = 0;
 pressingSpell4 = true;
 [self releaseR];
 }
 
 //pressingSpell1 = keyQPressed;
 //pressingSpell2 = keyWPressed;
 //pressingSpell3 = keyEPressed;
 //pressingSpell4 = keyRPressed;
 
 [self timerLogic];
 }
 */
- (void)tapRecall {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressRecall];
        [self releaseRecall];
    });
}

- (void)tapQ {
    if (pressingSpell1) {
        qCountSimulatedReleases += 1;
        dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
             if (pressingSpell1) {
                 [self pressQ];
                 [self releaseQ];
             }
        });
    }
}
- (void)tapW {
    if (pressingSpell2) {
    wCountSimulatedReleases += 1;
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        if (pressingSpell2) {
        [self pressW];
        [self releaseW];
        }
    });
    }
}
- (void)tapE {
    if (pressingSpell3) {
    eCountSimulatedReleases += 1;
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        if (pressingSpell3) {
        [self pressE];
        [self releaseE];
        }
    });
    }
}
- (void)tapR {
    if (pressingSpell4) {
    rCountSimulatedReleases += 1;
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        if (pressingSpell4) {
        [self pressR];
        [self releaseR];
        }
    });
    }
}


/*
- (void)tapSpell1 {
    qCountSimulatedReleases += 1;
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressQ];
        [self releaseQ];
    });
    
}
- (void)tapSpell2 {
    wCountSimulatedReleases += 1;
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressW];
        [self releaseW];
    });
}
- (void)tapSpell3 {
    eCountSimulatedReleases += 1;
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressE];
        [self releaseE];
    });
}
- (void)tapSpell4 {
    rCountSimulatedReleases += 1;
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressR];
        [self releaseR];
    });
}*/
- (void)tapActive1 {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressActive1];
        [self releaseActive1];
    });
}
- (void)tapActive2 {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressActive2];
        [self releaseActive2];
    });
}
- (void)tapActive3 {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressActive3];
        [self releaseActive3];
    });
}
- (void)tapActive5 {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressActive5];
        [self releaseActive5];
    });
}
- (void)tapActive6 {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressActive6];
        [self releaseActive6];
    });
}
- (void)tapActive7 {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressActive7];
        [self releaseActive7];
        
    });
}
- (void)tapWard {
    dispatch_async(dispatch_get_global_queue( DISPATCH_QUEUE_PRIORITY_HIGH, 0), ^(void){
        [self pressWard];
        [self releaseWard];
        
    });
}


- (void)pressRecall {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 11, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseRecall {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 11, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)pressQ {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 12, YES);
    CGEventPost(kCGSessionEventTap, event);
    CFRelease(event);
}
- (void)releaseQ {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 12, NO);
    CGEventPost(kCGSessionEventTap, event);
    CFRelease(event);
}

- (void)pressW {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 13, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseW {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 13, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}

- (void)pressE {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 14, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseE {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 14, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}

- (void)pressR {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 15, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseR {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 15, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}


/*
 - (void)pressSpell1 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 12, YES);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }
 - (void)releaseSpell1 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 12, NO);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }
 - (void)pressSpell2 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 13, YES);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }
 - (void)releaseSpell2 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 13, NO);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }
 - (void)pressSpell3 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 14, YES);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }
 - (void)releaseSpell3 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 14, NO);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }
 - (void)pressSpell4 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 15, YES);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }
 - (void)releaseSpell4 {
 CGEventRef event = CGEventCreateKeyboardEvent(NULL, 15, NO);
 CGEventPost(kCGHIDEventTap, event);
 CFRelease(event);
 }*/
- (void)pressActive1 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 18, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseActive1 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 18, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)pressActive2 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 19, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseActive2 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 19, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)pressActive3 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 20, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseActive3 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 20, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)pressWard {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 21, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseWard {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 21, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}

- (void) pressMouseRight:(int)x y:(int)y {
    CGEventRef theEvent = CGEventCreateMouseEvent(NULL, kCGEventRightMouseDown, CGPointMake(x, y), kCGMouseButtonRight);
    CGEventSetType(theEvent, kCGEventRightMouseDown);
    CGEventPost(kCGHIDEventTap, theEvent);
    CFRelease(theEvent);
}
- (void) releaseMouseRight:(int)x y:(int)y {
    CGEventRef theEvent = CGEventCreateMouseEvent(NULL, kCGEventRightMouseUp, CGPointMake(x, y), kCGMouseButtonRight);
    CGEventSetType(theEvent, kCGEventRightMouseUp);
    CGEventPost(kCGHIDEventTap, theEvent);
    CFRelease(theEvent);
}


- (void)pressActive5 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 22, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseActive5 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 22, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)pressActive6 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 23, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseActive6 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 23, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)pressActive7 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 0x1A, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseActive7 {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 0x1A, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}

- (void)pressD {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 2, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseD {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 2, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)pressF {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 3, YES);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}
- (void)releaseF {
    CGEventRef event = CGEventCreateKeyboardEvent(NULL, 3, NO);
    CGEventPost(kCGHIDEventTap, event);
    CFRelease(event);
}



- (void)createTap
{
    CGEventMask        eventMask;
    CFRunLoopSourceRef runLoopSource;
    
    // Create an event tap. We are interested in key presses.
    //kCGKeyboardEventAutorepeat;
    //kCGEventKeyUp;
    eventMask = ((1 << kCGEventKeyDown) | (1 << kCGEventKeyUp)); // | (1 << kCGEventFlagsChanged)
    //kCGEventTapOptionDefault
    eventTap = CGEventTapCreate(kCGSessionEventTap, kCGHeadInsertEventTap, 0,
                                eventMask, myCGEventCallback, NULL);
    if (!eventTap) {
        fprintf(stderr, "failed to create event tap\n");
        exit(1);
    }
    
    // Create a run loop source.
    runLoopSource = CFMachPortCreateRunLoopSource(kCFAllocatorDefault, eventTap, 0);
    
    // Add to the current run loop.
    CFRunLoopAddSource(CFRunLoopGetCurrent(), runLoopSource,
                       kCFRunLoopCommonModes);
    
    // Enable the event tap.
    CGEventTapEnable(eventTap, true);
    
    // Set it all running.
    CFRunLoopRun();
    
}
- (void)createTapMouse
{
    CFMachPortRef      mouseEventTap;
    CGEventMask        eventMask;
    CFRunLoopSourceRef runLoopSource;
    
    // Create an event tap. We are interested in key presses.
    eventMask = ((1 << kCGEventMouseMoved) | (1 << kCGEventLeftMouseDown) | (1 << kCGEventLeftMouseDragged)  | (1 << kCGEventLeftMouseUp)  | (1 << kCGEventRightMouseDown) | (1 << kCGEventRightMouseDragged)  | (1 << kCGEventRightMouseUp));
    mouseEventTap = CGEventTapCreate(kCGSessionEventTap, kCGHeadInsertEventTap, 0,
                                eventMask, myCGEventCallbackMouse, NULL);
    if (!mouseEventTap) {
        fprintf(stderr, "failed to create event tap for mouse\n");
        exit(1);
    }
    
    // Create a run loop source.
    runLoopSource = CFMachPortCreateRunLoopSource(kCFAllocatorDefault, mouseEventTap, 0);
    
    // Add to the current run loop.
    CFRunLoopAddSource(CFRunLoopGetCurrent(), runLoopSource,
                       kCFRunLoopCommonModes);
    
    // Enable the event tap.
    CGEventTapEnable(mouseEventTap, true);
    
    // Set it all running.
    CFRunLoopRun();
    
}

- (BOOL)applicationShouldTerminateAfterLastWindowClosed:(NSApplication *)theApplication {
    return YES;
}







- (IBAction)qPreactivateW:(id)sender {
    qPreactivateW = !qPreactivateW;
}
- (IBAction)qPreactivateE:(id)sender {
    qPreactivateE = !qPreactivateE;
}
- (IBAction)qPreactivateR:(id)sender {
    qPreactivateR = !qPreactivateR;
}

- (IBAction)wPreactivateQ:(id)sender {
    wPreactivateQ = !wPreactivateQ;
}
- (IBAction)wPreactivateE:(id)sender {
    wPreactivateE = !wPreactivateE;
}
- (IBAction)wPreactivateR:(id)sender {
    wPreactivateR = !wPreactivateR;
}

- (IBAction)ePreactivateQ:(id)sender {
    ePreactivateQ = !ePreactivateQ;
}
- (IBAction)ePreactivateW:(id)sender {
    ePreactivateW = !ePreactivateW;
}
- (IBAction)ePreactivateR:(id)sender {
    ePreactivateR = !ePreactivateR;
}

- (IBAction)rPreactivateQ:(id)sender {
    rPreactivateQ = !rPreactivateQ;
}
- (IBAction)rPreactivateW:(id)sender {
    rPreactivateW = !rPreactivateW;
}
- (IBAction)rPreactivateE:(id)sender {
    rPreactivateE = !rPreactivateE;
}

- (IBAction)wardHopOnOff:(id)sender {
    wardHopOn = !wardHopOn;
}
- (IBAction)wardHopKeyChanged:(NSComboBox*)sender {
    wardHopKey = [[sender stringValue] characterAtIndex:0];
    [self removeFocus];
}


int getTimeInMilliseconds(int64_t absoluteTime)
{
    const int64_t kOneMillion = 1000 * 1000;
    static mach_timebase_info_data_t s_timebase_info;
    
    if (s_timebase_info.denom == 0) {
        (void) mach_timebase_info(&s_timebase_info);
    }
    
    // mach_absolute_time() returns billionth of seconds,
    // so divide by one million to get milliseconds
    return (int)((absoluteTime * s_timebase_info.numer) / (kOneMillion * s_timebase_info.denom));
}





@end
