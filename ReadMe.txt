Idea of this exersise to get data from the table, which name provided by user, 
with the condition, that schema (files names) are exactly the same.
So I tried to use EF for this, with reflection and found it is difficult, because I need to use Reflection heavily.
Other option is just "switch" which will require a lot of hard coding. 
And the last version "with Generics" in progress..

For a moment, I tried to inherit model classes from common class, but they got overriden.

Note: use attached .sql file in order to generate tables used in this example

C# 7 and SQL 2017 used