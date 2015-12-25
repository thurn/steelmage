# Decisions
 - ASCII-art graphics, very minimal
 - Nothing is procedurally generated or random
 - Ayanor worldship setting
 - Four gods (Aeris, Petra, Ignis, Aquis) represent the four types of magic
 - Two modes: narrative mode and encounter mode
   - game divided into areas, each area contains several encounters
   - auto-save and fully heal when you leave an area
   - draw cards when you enter encounter mode
   - Modes switch automatically if you see a hostile enemy, get attacked, or attack somebody
   - Mode switches back when no hostiles aware of your presence
   - Can't change equipment or deck during encounter.
   - Can change both outside of encounter
   - Items can be fixed-use or per-combat, but everything resets after encounter is over.
   - Start generating mana at beginning of encounter, begin with 50% mana
   - Some healing after encounter (based on healing spells in deck)
   - Full healing only at auto-save points
 - Rolling a d20 actually pulls a value from a deck of 30 cards numbered:
   [1,2,3,4,5,6,6,7,7,8,8,9,9,10,10,11,11,12,12,13,13,14,14,15,15,16,17,18,19,20]
 - Whenever possible, put mechanics on cards, not in rules
 - No races or classes, but "items" are actually cybernetic enhancements
 - No carry limits, can carry an arbitrary amount of stuff, can convert it to currency
 - No "planes", but teleportation and stuff is possible
 - Spells have subtypes, e.g. "fire", "mind control", "illusion"
   - Spells also require hands or verbal commands to execute
   - Silence effects or inability to use hands can prevent them
 - "Detect magic" type spells figure out what's magic and what the subtype is.
 - No alignment, creatures, spells, some items associated with specific gods
 - You can "lock" cards. Your starting hand at beginning of encounter mode depends on locks:
   - 3 locked cards
   - 2 locked cards, 2 random cards
   - 1 locked card, 4 random cards
   - 0 locked cards, 6 random cards
 - Maximum hand size is 6, draw 1 at beginning of turn, discard down to 6 at end of turn
 - At beginning of encounter, there is a "reaction round" where both sides can take reactions
  - Whichever side has the element of surprise goes first in the reaction round
  - And in subsequent rounds
 - "Saving throws" are just normal skill or attribute checks to mitigate some effects
 - Three attributes:
   - Mind (intellect, charisma, wisdom, cunning, presence, knowledge)
   - Fortitude (strength, constitution, brawn, might)
   - Agility (dexterity, speed)
 - Fortitude governs base HP
 - Agility governs aim
 - Six skills: Athletics, Acrobatics, Mechanics, Perception, Stealth, Influence
 - Attribute check is "roll lower than attribute score with modifiers"
 - Skills just add modifiers to certain attribute checks
 - Checks can have various circumstantial bonuses, e.g. assistance from others
 - Up to 10 levels in each skill
 - Each level gives you a +1 bonus to checks of that type
 - No formal level up, can find cards that give permanent bonuses to the team
  - 4 skill points per memeber
  - 1 attribute point per member
  - extra health
 - Level up 10 times
 - Quantities that can be damaged:
  - health
  - ability scores
  - max health
  - max mana
  - movement speed
  - skills (avoid)
- No "caster levels"
  - "Caster level bonus" can be represented via bonus mana which you can use for a specific purpose
- Spells can be illusions or mind-affecting, it's a Mind attribute save to resist
- Anti-magic fields negate magic
- Body slots: Throat, Face, Head, Arms, Hands, Torso, Shoulders, Waist, Fingers (x2)
- Armor implements "damage reduction", subtracts a certain amount from attacks
- Can also have type-specific damage reduction, e.g. fire damage
- Can also be totally immune to damage types
- Items should have cool backstories
- No berserker rage, could be a spell
- No weapon or spell customization, hard to balance
- Money system, weapons and spells can be automatically converted into money
- Can buy weapons and spells (at a markup)
- No item weight or inventory limits
- Temporary mana exists
- Can store mana in items
- Intelligent weapons could exist
- No flanking (beyond normal cover rules)
- No identifying items
- Area attacks can't miss, shots can miss but can't hit allies
- Some enemies have defense, which means they're harder than normal to hit, penalty to hit
- No tracking enemies
- Some items are associated with one of the four gods, only have effect (or good effect) for that god's followers
- Riding mounts would be an option, just an athletics check
- Creature attacks are cards, can have types, e.g. gaze, breath
- Some weapons can stun enemies, no formal subdual damage
- Falling damage is a thing
- All creatures have senses, can rely on them to detect enemies
  - sight
  - hearing
  - scent
  - special (tremorsense, echolocation, magical)
  - Creatures have a primary sense they use to locate enemies
  - Can be "blinded" by e.g. loud noise or horrible smells
  - Often not affected by invisibility
- All actions create noise,e tc
- Stealth exists, attacking from stealth gives first turn in combat
- No "touch attack" or "ranged touch attack" concept
- Dying: probably just bleed out for 4-8 rounds based on fort?
- Death is permanent unless you revert to an earlier save or resurrect
- Can save on demand when not in encounter mode, auto-saves at periodic intervals
- "Iron Man Mode" is an option, makes death permanent
- Improvised attacks, unarmed attacks, throwing stuff: athletics checks
- Darkness and light sources are a big deal, don't need to carry torches though, can turn off lights
- Light makes characters easier to see coming
- Invisibility is a thing, rules for fighting invisible enemies
- Various damage types, added as needed, fire, acid, electric, cold, sonic at minimum
- Magical interactions: rain puts out fire
  * Stack-like system for effects, last write wins. E.g. casting a fire spell on some
    water evaporates it, casting a water spell on some fire puts it out.
  * difficult terrain, slippery terrain, wet stones
  * Object/Tile Effects: On Fire, Ice-covered, Movable, In Darkness, Broken, Destroyed
    Waterlogged, water electrified, acid, snow-covered, oil-covered, metallic, wooden, living,
    wind, silence, web-covered, dessicated (water drained), raining, raining acid, bright light,
    antimagic, smoke, black flames, mist/fog, spikes, cold, loud noise, blocked with rocks,
    volcanic eruption, aura of fear, magical walls (force, clear glass, etc), glyph which has
    area effects, silver glow, rain of rocks, hail, zone of warding, sweet/scary melody, red fire
    which outlines creatures, heat drain area, hole in ground, mind defense, black hole, red
    seawater, no-attacks area, sand blast, covered in slime, vines burst out, make everything
    invisible visible. Extreme temperature: hot/cold. Sunny.
  * Interactions: electricity good against targets wearing metal
  * Stacking and opposition of interactions: 2 wind effects?
- Conditions: Fear, Blinded, Stunned, Entangled, Prone, Nauseated, Intoxicated, Infected, Dazed
  Sleep, Paralysis, Slow, Pinned, On Fire, Charmed, Frozen, Deafened, Shaken, Dazzled, Fatigued,
  Exhausted, Sickened, Blur, Displacement, Invisible, Poisoned, Confused, Diseased, Feebleminded,
  Insane, Prone, Cowering, Helpless, Distracted, Turned to Stone
- Can have condition immunity
- No formal ghosts or undead, similar concepts might appear as creature mechanics
- Units can change form, e.g. into a gas
- Fear & Morale system. Frightened, Panicked, Shaken
- Poison system. Get poisoned, cure poison.
- No concentration, but possible to distract enemies e.g. with noises
- No scrolls, potions can be a thing
- Effects on enemy proximity, e.g. glow when they're close
- No feinting in combat
- All objects are destructible, have damage reduction and hitpoints, flammability, conductivity, etc
- Damaging/Destroying enemy weapons or other items is possible
  - not fun for player, leave it for higher levels
- Stealing from enemies is possible, disarming enemies in combat is possible
- Creatures have various sizes, characters could change size
- Weapons usually exist at human scales and aren't used by giant creatures
- Can charge enemies (athletics), trip enemies (acrobatics)
- Not really any resting concept
- No alignment, only associations with specific gods
- Teleportation is a thing, can have errors
- Resurrection is a thing, can have penalties
- Counterspells are a thing, magic detection is a thing
- Critical hits are not a thing, but some weapons have a keyword mechanic
  - activates if you score 5 over the agility target for a hit
- Creature types: several, coarse to fine (like spells)
- Range categories: personal, touch, close, medium, long
  - shotgun: close range, rifle: medium range, sniper rifle: long range
- No turn undead
- No special materials, weapons could have bonuses vs. types though
- Items have damage reduction
- Secret doors are a thing, Perception check to find
- Diseases are a thing, like "filth fever"
- Traps are a thing, have trigger mechanism, trap mechanism, disable checks for both
- Close doors, hold doors closed
- Have an array of nouns you can ask anybody about
