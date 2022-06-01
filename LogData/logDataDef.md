# Log Line Definitions
This is very loosely based on the output of ACT (Advanced Combat Tracker) but due to the complexities of the log files I have decided to just use my own test data. Since a function app parses the data, it would be trivial to update the definitions as and when needed, as the API/Database is separate.

## Types of Log Line
Log lines begin with a number indicating the type of line, each type is detailed below:

### 0 Instance Finished
Format 0|{TimeStamp}

To allow ease of finishing a log

Example: 0|2022-05-31\[17:04:22\]

### 5 Character First Appears
Format 5|{TimeStamp}|{Name}|{Server}|{Job Class}

This line doesn't normally exist as such, but it is here for ease of discovering characters

Example: 5|2022-05-31\[17:04:22\]|Ryoka Lenares|Cerberus|Black Mage

### 6 Instance Name
Format 6|{TimeStamp}|{Instance Name}

This line again doesn't exist in this simple form, but is here for ease

Example: 6|2022-05-31\[17:04:22\]|Asphodelos: The Fourth Circle (Savage)

### 10 Damage Received
Format 10|{TimeStamp}|{Origin}|{AttackName}|{Target}|{Damage}

Example: 10|2022-05-31\[17:04:22\]|Chaos|Attack|Ryoka Lenares|15290

### 11 Death
Format 11|{TimeStamp}|{Origin}

Example: 11|2022-05-31\[17:04:22\]|Ryoka 

### 15 Single Target Attack Resolved
Format: 15|{TimeStamp}|{Origin}|{AttackName}|{Target}|{Damage}

Example: 15|2022-05-31\[17:04:22\]|Ryoka Lenares|Fire IV|Chaos|12304

### 16 Area of Effect Attack Resolved
Format 16|{TimeStamp}|{Origin}|{AttackName}|{Target}|{Damage}

An instance of this line will appear for each target affected

Example: 16|2022-05-31\[17:04:22\]|Ryoka Lenares|Flare|Chaos|8000

### 18 Single Target Heal Resolved
Format: 18|{TimeStamp}|{Origin}|{AttackName}|{Target}|{HealAmount}

Example: 18|2022-05-31\[17:04:22\]|Ryoka Lenares|Vercure|Freyja Lenares|2320

### 19 Area of Effect Heal Resolved
Format 19:{TimeStamp}|{Origin}|{AttackName}|{Target}|{HealAmount}

An instance of this line will appear for each target affected

Example: 19|2022-05-31\[17:04:22\]|Ryoka Lenares|Cure III|Freyja Lenares|13049

### 20 Healing Shield Applied
Format: 20|{TimeStamp}|{Origin}|{AttackName}|{Target}|{ShieldAmount}

Example: 20|2022-05-31\[17:04:22\]|Ryoka Lenares|Aspected Benefic|Freyja Lenares|8204

### 21 Raise Applied
Format: 21|{TimeStamp}|{Origin}|{Target}

Example: 21|2022-05-31\[17:04:22\]|Ryoka Lenares|Freyja Lenares

### 22 Damage over Time Effect Applied
Format: 21|{TimeStamp}|{Origin}|{AttackName}|{Target}|{Time}|{InitialDamage}|{variance}

Damage over time skills are somewhat random and each tick is not logged. I have simplified having to store data on each attack by including a parameter which will be the percentage variance from the initial damage for each tick (which is every 3 seconds). 

This will currently not be included.

Example: 18|2022-05-31\[17:04:22\]|Ryoka Lenares|Thunder III|Chaos|30|2054|20

### 23 Heal over Time Effect Applied
Format: 22|{TimeStamp}|{Origin}|{AttackName}|{Target}|{Time}|{InitialHeal}|{variance}

Damage over time skills are somewhat random and each tick is not logged. I have simplified having to store data on each attack by including a parameter which will be the percentage variance from the initial damage for each tick (which is every 3 seconds). 

This will currently not be included.

Example: 22|2022-05-31\[17:04:22\]|Ryoka Lenares|Aspected Benefic|FreyjaLenares|18|4031|20