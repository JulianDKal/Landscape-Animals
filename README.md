### Setup

- Instantiate playing field
- Introduce starting landscape (if needed)
- Spawn animals
- Introduce starting challenges

### Basic State Structure

- **Input:**
Take user's input in terms of (animal + direction)
	- if legal, proceed to Move state
	- else take another input
- **Move:**
Resolve the movement as following
	- change animal location
	- change previous landscape
	- change move counter (if we want to implement)
	- switch state to Challenges
- **Challenges:**
Each challenge tile checks surroundings
	- update any challenge counters
	- introduce new challenges as needed
	- switch to clean up
- **CleanUp:**
	- remove any expired animals
	- add new animals as needed
	- switch to input
