Dashboard Template
==================

Synopsis
--------

This is a basic Vidusl Studio 2012 solution that establishes the ground work for
a [Multitier Architecture][] website.

Architecture Overview
---------------------

Here is a very simple outline of how the solution is setup:

* **Dashboard** (Solution)
    * **1. Presentation**
        * WebUI (ASP.NET MVC)
    * **2. Services**
        * Services (Class Library)
    * **3. Data Access Layer**
        * DAL (Class Library)
    * **4. Transverse**
        * Core (Class Library)
        * Infra (Class Library)

The projects relate to each other in the following way:

    +---+  +--------------+  +---+
    |   |--| Presentation |--|   |
    |   |  +--------------+  |   |
    | I |         |          | C |
    | n |  +--------------+  | o |
    | f |--|   Services   |--| r |
    | r |  +--------------+  | e |
    | a |         |          |   |
    |   |  +--------------+  |   |
    |   |--| Data  Access |--|   |
    +---+  +--------------+  +---+

Solution Dependancies:
----------------------

* [Code Contracts][] (See [this post][Code Contracts Help] for guidance if you're having problems)

  [Code Contracts]: http://msdn.microsoft.com/en-us/devlabs/dd491992.aspx "Code Contracts for .NET"
  [Code Contracts Help]: http://blogs.msdn.com/b/bclteam/archive/2010/01/26/i-just-installed-visual-studio-2010-now-how-do-i-get-code-contracts-melitta-andersen.aspx
  [Multitier Architecture]: http://en.wikipedia.org/wiki/Multitier_architecture "Multitier Architecture"