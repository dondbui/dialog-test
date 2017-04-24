# dialog-test
This is my own experiment in parsing Yarn JSON files for conversation systems without going fully into the Yarn/Twine scripting language.

### Reasoning
- The Twine/Yarn dialog node graph editor is a great intuitive way to edit and create dialog.
- Though very powerful the scripting system that comes with YarnSpinner may be a bit excessive for my purposes.
- The JSON that is outputted by Yarn is very very clean which is a good thing.
- I want to create my own simpler dialog system fit for my own personal projects.
- I thought architecting my own dialog system would be fun.

### Current Conversation Node Structure
- **title**: The title id for this node. Not really used in my system for anything more than as a unique identifier.
- **body**: The actual body of text for this conversation node. Links to other nodes and parameter modification occurs here.
- **tags**: Tags applied to each node can help indicate specific assumptions for this node. For example, we can define a node as starting node by tagging "start"

#### Supported Conversation Node Tags
- **choice**: This is node where a choice will be made
- **end**: This node is an ending node.
- **start**: This is a starting node.
 
### Currently Working
- Loading of the Yarn JSON file
- Parsing the Yarn JSON into my own Conversation object using Unity's [JSONUtility](https://docs.unity3d.com/ScriptReference/JsonUtility.html)
  - Creates Dictionary of title ids to conversation nodes
  - Creates mapping of tags to List of conversation nodes
- Parsing of the ConversationNode
  - Parse choice linkages to other nodes
  - Parse parameter modifiers
- Starting a conversation
- Stepping through a conversation
- Ability to make a decision in a conversation

### Pending Implementation
- Saving of Conversation Choices
- Applying parameter modifiers upon viewing a conversation node


### Links
- Yarn Spinner: https://github.com/thesecretlab/YarnSpinner
- Yarn: https://github.com/infiniteammoinc/Yarn