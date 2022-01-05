  _____ _                 _                 _____      _                              
 / ____| |               | |               |  __ \    | |                             
| (___ | | ___  ___ _ __ | | ___  ___ ___  | |__) |__ | | _____ _ __ ___   ___  _ __  
 \___ \| |/ _ \/ _ \ '_ \| |/ _ \/ __/ __| |  ___/ _ \| |/ / _ \ '_ ` _ \ / _ \| '_ \ 
 ____) | |  __/  __/ |_) | |  __/\__ \__ \ | |  | (_) |   <  __/ | | | | | (_) | | | |
|_____/|_|\___|\___| .__/|_|\___||___/___/ |_|   \___/|_|\_\___|_| |_| |_|\___/|_| |_|
                   | |                                                                
                   |_|                                                                


=================
README:
=================


This document refers to BETA V1.0 version of the Sleepless Pokemon Trainer Sheet. This manual hopes to talk you through the basic use of the program in it's current form, and provide some understanding of what it is currently capable of doing. Suggestions and feedback are welcome, although please collate feedback together as to avoid bombing me with messages.
Items marked (WIP) are currently in progress.
Items marked (NA) are currently not avaliable at all.

You may notice most of the trainer specific features are (WIP), as the focus of this release was the pokemon party system.
Each of the different pages will pop out in their own window, so you can move and use multiple windows as you need. If you wish to close the program, close the main window.

The descriptions are first, followed by the getting started guide.

=================
Home page:
=================

The home page is where i expect trainers will spend most of their time. It has a breif overview of all the different areas of the character sheet. 

Top left is the Trainer summary, which includes stats, characteristics and description.
below that is Trainer level, and the box of assigned trainer classes (WIP)
Gym Badges will list all champion gyms and frontier brain awards. (WIP)
Ribbons will list all contest ribbons won for the trainer at their highest level, which pokemon won them, and when (WIP)
The large box in the middle will list all of the current trainer features possessed (WIP) with the option to right click an item to get more information about it (NA)
The box on the right will display the pokemon in the player party, and at a quick glance display key data. 
The white box at the bottom displays character notes, and the Test Mode button will be repurposed to save and update the notes section.
the TEST MODE button currently toggles  between test and live features, allowing you to access restricted (disabled) buttons in case you wish to peek at WIP sections, although no support is offered for those areas yet.

=================
Trainer page:
=================

The Trainer page is currently the control area of the character sheet. I appreciate it's pretty ugly right now, but I was focusing primarily on function for this release, with the idea being I can go back and make things look nice if the ideas here work and functionality isn't too clunky.
The left hand side is divided up into four sections. 
Loading full data allows you to select the relevant trainer for use in your sheet. By doing it this way it allows you to see all the possible PC/NPC characters, and swap between them as needed without having to load everyones entire dex onto your PC every time you load the program. 
Unload data allows you to return a loaded character back to it's stub form, which will free up space for the program to run. This feature is primarily aimed for GMing a session, as it means you don't end up with 40GB of RAM used because of the 100's of NPC's you loaded.
active trainer is how you swap the character which is currently present on your character sheet. Note only one character can be active at a time. If no active trainer is selected, the trainer actions will not work. 
 = = ALL TRAINER ACTIONS ARE SET AGAINST THE ACTIVE TRAINER. 
finally, Reload all Data Files will reload all of the local data. (WIP). While this does currently reload all data, it does not currently refresh everywhere, so expect some bugs while I iron this one out. Testing is welcome.
 
The right hand pane is divided up into four tabs.
TRAINER ADVANCEMENTS will advance your active trainer (NA)
NEW TRAINER will let you create a new pokemon trainer at level 0. Starting classes are selected at level 1. Please make sure to enter a value in every box.
NEW POKEMON will let you add a new pokemon to your team. You do not need to specify a breeder or parent pokemon if you do not possess them. Note that you cannot breed with pokemon you do not own. This is currently intentional, although I may edit to allow team breeding if it's strongly desired (It''s not that I don't want to allow it, it's just a ballache to program).
POKEMON ADVANCE is how you advance your pokemon thorugh levels. You start by selecting a pokemon, and then choose which bonus to apply. The Level Up section will automatically apply both the bonus and the level increment. The Move Tutor is how you teach your pokemon new moves (Note it does not split the moves by slot - you need to do that manually currently due to limitations) and the Custom Advancement is my answer to "Feature X says I can do Y" -> Apply a Custom Advancement, and set the value to what you need/want. I intend to provide a reference table in the encyclopedia later when I have time as to what each value maps to (for example, Tpye ID 19 is NOTYPE, and 14 is Dragon).
 
 
=================
PokeDex page:
=================

The pokedex is pretty self descriptive. You will be provided with a code by your GM that is 5, 8 or 12 characters in length. Place that code in the CODE box at the bottom, and press Search. for example you can use 1DA1-E011-0000 to look at a Sassy Leafeon. 
 
=================
Party page:
=================

The Party page will show you summary cards of your active party. The first six slots are red and display active party members. The last two slots are green and reserved for guest/companion pokemon. 
The bars in the lower half of the card show the rough stat distribution of the pokemon compared to the maximum values (Under review). 
There are small icons that can appear on the bottom of the card to indicate status effects (WIP)
You can click the green button on the pokeball to go to the detailed pokemon page.

The pokemon detailed page should be prety self evident, the buff/debuff alters the stat based on the buffing system, and displays the current buff stack in between it. It is worth noting that there is NO CAP on buff and debuffs applied on this page intentionally, as I wanted to allow for GM's to apply modifiers and support it in program. 
the NOTE box allows storage of notes against the pokemon (WIP - it displays the notes from creation, but they can't currently be updated by yourself, I have to do it in the admin)
Pokemon moves are displayed on the right, Natural moves on the top, and artificial moves on the bottom.


=================
Inventory (Bag) page:
=================

This will allow for storage and use of items (NA)

=================
GM page:
=================

This page allows for generation of random encounters and pokedex lookups.
IMPORTANT NOTE. If you close the detailed pokemon page generated, there is no way to get back to it.


=================
PC page:
=================

This shows you a list of all pokemon owned by the active trainer on the left, and the current party details on the right. 
you cannot look at more details on the pokemon without adding to the party due to limitations.
You add to the party by selecting the pokemon, which slot you want it in, and pressing add. 
You remove from the party by selecting the pokemon in the party list and then pressing remove. 
I can't use the same interface for add/remove sadly due to the method used for display. I've put both types in so you can try them and provide feedback on if it's okay to leave both, or if you want a box to remove pokemon the same as add. 


=================
Encyclopedia page:
=================

This will allow you to look up rules, read this readme, and check other data (NA).


=================
Save page:
=================

Depreciated (NA)

=================
Load page:
=================

Depreciated (NA)


=================
~~~~~~~~~~~~~~~~~
=================
GETTING STARTED:
=================
~~~~~~~~~~~~~~~~~
=================

Double click PokemonRPG.exe to begin. 
The program will then contact MelonChan and pull down all your needed data. If you get any popups saying "Missing X Data", please let me know.
Once the main page loads, click on TRAINER.
If you have not made a trainer yet, click NEW TRAINER and fill out the information for your new trainer. Once you have pressed submit, it will say "Operation Completed" - you will need to restart the program to continue. 
Select your trainer from the LOAD TRAINER BOX, and then press LOAD.
Click the dropdown in ACTIVE TRAINERS, select your trainer, and press SET ACTIVE TRAINER.
It will then inform you that you need to right click the blue trainer card on the main page. Do so, and your trainer is now loaded. You can now use the sheet as required. 