using UnityEngine;
using System.Collections;

public class DecisionTree : MonoBehaviour {

	DTree<BranchLogic> dPath;
	AIBehavior methodLibrary;
	public string behaviorType;

	public DecisionTree(string type){

		behaviorType = type;

	}
		
	void Start () {	
		methodLibrary = gameObject.GetComponent<AIBehavior> ();
		dPath = new DTree<BranchLogic> (new BranchLogic(methodLibrary.MaxWander));
		coreTreeSetup ();
	}

	public void startDeciding(){
		InvokeRepeating ("makeDecision", 1, 0.4f);
	}

	public void stopDeciding(){
		CancelInvoke ("makeDecision");
	}

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

	public void pathToSpawn(){
		dPath.AddChild(new BranchLogic (methodLibrary.AtDest));
		dPath.GetChild(1);
		dPath.AddChild(new BranchLogic (methodLibrary.ReturnToSpawn));
		dPath.GetRoot();
	}

	public void attackLogicBasic(){
		dPath.AddChild (new BranchLogic (methodLibrary.InRange));
		dPath.GetChild(1);
		dPath.AddChild (new BranchLogic (methodLibrary.Attack));
		dPath.AddChild (new BranchLogic (methodLibrary.AdvanceTowards));
		dPath.GetRoot();
	}
}
