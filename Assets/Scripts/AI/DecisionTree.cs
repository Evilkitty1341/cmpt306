using UnityEngine;
using System.Collections;

public class DecisionTree : MonoBehaviour {

	DTree<BranchLogic> dPath;
	AIBehavior methodLibrary;
	public string behaviorType;

	public DecisionTree(string type){

		behaviorType = type;

	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Make and instance of the tree based ont he AI type assigned, if no type matches give it the default mob AI.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Start () {	
		methodLibrary = gameObject.GetComponent<AIBehavior> ();

		if(behaviorType == "mob"){
			dPath = new DTree<BranchLogic> (new BranchLogic(methodLibrary.MaxWander));
			coreTreeSetup ();
		}
		else if(behaviorType == "boss"){
			dPath = new DTree<BranchLogic> (new BranchLogic(methodLibrary.MaxWander));
			coreTreeSetup ();
		}
		else{
			dPath = new DTree<BranchLogic> (new BranchLogic(methodLibrary.MaxWander));
			coreTreeSetup ();
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Start the tree deciding logic. 
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void startDeciding(){
		InvokeRepeating ("makeDecision", 1, 0.4f);
	}


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Stope the tree deciding logic.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void stopDeciding(){
		CancelInvoke ("makeDecision");
	}


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		While at a step in the tree make a decision. Decisions are stored in BranchLogic so results can be checked
		from various parts of the code without having to pass references. The tree then encapsulates the logic.
		Once a decision is made, it moves the tree to appropriate node.
		Reference:
		Result = 1, 2, 3...
		Move to child (1, 2, 3...) etc.

		Result = 0
		Move to Root of the whole tree. Used for leaf nodes and bypass logic.

		Result = -1
		Move to parent of the current node.

		Result = -2
		Move to child 1 of the parent of the current node. Used for a depth - 1 bypass to the other BINARY decision.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void makeDecision(){

		dPath.GetElement ().chooseBranch ();
		//Debug.Log (dPath.GetElement ().taskResult ().ToString ());
		if (dPath.GetElement ().taskResult () > 0) {
			dPath.GetChild (dPath.GetElement ().taskResult ());
		}
		else if (dPath.GetElement().taskResult() == -1){
			dPath.GetParent();
		}
		else if (dPath.GetElement().taskResult() == -2){
			dPath.GetParent ();
			dPath.GetChild (1);
		}
		else if (dPath.GetElement ().taskResult() == 0){
			dPath.GetRoot();
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Core tree setup.
		After root is intialized, it adds the all children and leaf nodes associated with basic AI.
		Use Get(Child/Parent/Root) to move the iterator to that node. Also returns the stored branch logic reference
		if required.
		
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void coreTreeSetup(){
		//ROOT
		dPath.GetRoot();
		//ROOT Children
		dPath.AddChild (new BranchLogic(methodLibrary.ReturnToSpawn)); //Leaf Node
		dPath.AddChild (new BranchLogic(methodLibrary.InThreatZone));
		//Child2
		dPath.GetChild (2);
		dPath.AddChild (new BranchLogic(methodLibrary.AllyDis));
		dPath.AddChild (new BranchLogic(methodLibrary.RandomTrinary));
		//Child2 -> Child1
		dPath.GetChild (1);
		dPath.AddChild (new BranchLogic (methodLibrary.HPCheck));
		dPath.AddChild (new BranchLogic (methodLibrary.HPCheckCautious));
		//Child2 -> Child2
		dPath.GetParent();
		dPath.GetChild (2); 
		dPath.AddChild (new BranchLogic (methodLibrary.Idle)); //leaf
		dPath.AddChild (new BranchLogic (methodLibrary.RandomWalk)); //leaf
		dPath.AddChild (new BranchLogic (methodLibrary.Taunt)); //leaf
		//Child2 -> Child1 -> Child1
		dPath.GetParent ();
		dPath.GetChild (1);
		dPath.GetChild (1);
		dPath.AddChild (new BranchLogic (methodLibrary.InRange));
		dPath.AddChild (new BranchLogic (methodLibrary.RunAway));
		dPath.GetChild (1);
		//AttackChoice
		dPath.AddChild (new BranchLogic (methodLibrary.Attack));
		dPath.AddChild (new BranchLogic (methodLibrary.AdvanceTowards));
		dPath.GetParent();
		dPath.GetParent();
		dPath.GetChild(2);
		dPath.AddChild (new BranchLogic (methodLibrary.InRange));
		dPath.AddChild (new BranchLogic (methodLibrary.RunAway));
		dPath.GetChild(1);
		dPath.AddChild (new BranchLogic (methodLibrary.Attack));
		dPath.AddChild (new BranchLogic (methodLibrary.AdvanceTowards));
		dPath.GetParent ();

		dPath.GetRoot ();

	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		A small 2 node sub tree to execute bypass pathing logic.
		Used exclusively to return to spawn.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void pathToSpawn(){
		dPath.AddChild(new BranchLogic (methodLibrary.AtDest));
		dPath.GetChild(1);
		dPath.AddChild(new BranchLogic (methodLibrary.ReturnToSpawn));
		dPath.GetRoot();
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Adds in a 3 node basic attack logic.
		If in range, attack. Otherwise advance a step to be in range.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void attackLogicBasic(){
		dPath.AddChild (new BranchLogic (methodLibrary.InRange));
		dPath.GetChild(1);
		dPath.AddChild (new BranchLogic (methodLibrary.Attack));
		dPath.AddChild (new BranchLogic (methodLibrary.AdvanceTowards));
		dPath.GetRoot();
	}
}
