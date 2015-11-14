using UnityEngine;
using System.Collections;

public class DecisionTree : MonoBehaviour {

	DTree<BranchLogic> dPath;
	AIBehavior methodLibrary;
	string behaviorType;

	public DecisionTree(string type){

		behaviorType = type;

	}
		
	void Start () {	
		methodLibrary = gameObject.GetComponent<AIBehavior> ();
		dPath = new DTree<BranchLogic> (new BranchLogic(methodLibrary.MaxWander));
		coreTreeSetup ();
	}

	public void startDeciding(){
		InvokeRepeating ("makeDecision", 1, 1);
	}

	public void stopDeciding(){
		CancelInvoke ("makeDecision");
	}

	void makeDecision(){

		dPath.GetElement ().chooseBranch ();
		//Debug.Log (dPath.GetElement ().taskResult ().ToString ());
		if (dPath.GetElement ().taskResult () > 0) {
			dPath.GetChild (dPath.GetElement ().taskResult ());
		}
		else {
			dPath.GetRoot();
		}
	}

	void coreTreeSetup(){
		//ROOT Children
		dPath.AddChild (new BranchLogic(methodLibrary.ReturnToSpawn)); //Leaf Node
		dPath.AddChild (new BranchLogic(methodLibrary.InThreatZone));
		//Child2
		dPath.GetChild (2);
		dPath.AddChild (new BranchLogic(methodLibrary.HPCheck));
		dPath.AddChild (new BranchLogic(methodLibrary.RandomTrinary));
		//Child2 -> Child1
		dPath.GetChild (1);
		dPath.AddChild (new BranchLogic (methodLibrary.AllyDis));
		dPath.AddChild (new BranchLogic (methodLibrary.RunAway));
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
		//TODO Need a second method for cautious range checking.
		dPath.AddChild (new BranchLogic (methodLibrary.InRange));
		//These will be replaced by specialized functions.
		//Child2 -> Child1 -> Child1 -> Child1
		dPath.GetChild (1);
		dPath.AddChild (new BranchLogic (methodLibrary.Attack));
		dPath.AddChild (new BranchLogic (methodLibrary.AdvanceTowards));
		//Child2 -> Child1 -> Child1 -> Child2
		dPath.GetParent ();
		dPath.GetChild (2);
		dPath.AddChild (new BranchLogic (methodLibrary.Attack));
		dPath.AddChild (new BranchLogic (methodLibrary.AdvanceTowards));
		dPath.GetRoot ();
	}
}
