using System.Collections;
using System.Collections.Generic;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/* 
	This is the element for the tree. It is kept seperate for tree reusage if necessary.
	This was encapsulated away from other scripts so that this process was self contained.
	This said, it could very well be moved to DecisionTree. 
*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
public class BranchLogic{
	
	public delegate int Function();
	Function execute;
	Function next;
	int result;
	
	
	public BranchLogic(Function set){
		execute = set;
		result = -1;	
	}
	
	public void chooseBranch(){
		result = execute ();
	}
	
	public Function task(){
		return next;
	}
	
	public int taskResult(){
		return result;
	}
	
}

delegate void TreeTracker<T>(T nodeContent);

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/* 
	Node for the tree.
	Contains the content to be stored AKA BranchLogic Instance
	A linked list of all children.
	A bool to indicate if this is the root node. (rather then looking out to the parent for that info)
	Constructor sets these values as appropriate.
*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
class DNode<T>{
	public T content;
	public DNode<T> parent;
	public LinkedList<DNode<T>> children;
	bool thisIsRoot;


	public DNode(T content, bool isRoot){
		this.content = content;
		if (isRoot) {
			thisIsRoot = true;
		} else {
			thisIsRoot = false;
		}
		children = new LinkedList<DNode<T>> ();
	}
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/* 
	Fairly straightforward tree implementation.
	Contains reference to root, and a pointer for the current node.
	Getting a child at index, returns the value at that index, and moves the pointer there.
	AddChild adds a child to the current node.
	GetRoot sets pointer to root. (We've hit a leaf node and are done with it.)
	GetElement returns the value at current without changing the pointer.
	Traverse allows you to traverse all children, not required but standard for a tree.
*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
class DTree<T>{

	private DNode<T> current;
	private DNode<T> root;

	public DTree(T content){
		root = new DNode<T>(content, true);
		current = root;
		root.parent = root;
	}

	public void AddChild(T content){
		DNode<T> temp = new DNode<T> (content, false);
		current.children.AddLast (temp);
		temp.parent = current;
	}

	public T GetChild(int num){
		foreach (DNode<T> n in current.children)
			if (--num == 0) {
				current = n;
				return current.content;
			}
		return root.content;
	}

	public T GetRoot(){
		current = root;
		return current.content;
	}

	public T GetParent(){
		current = current.parent;
		return current.content;
	}

	public T GetElement(){
		return current.content;
	}

	public void Traverse(DNode<T> node, TreeTracker<T> tracker){
		tracker (node.content);
		foreach (DNode<T> kid in node.children)
			Traverse (kid, tracker);
	}
}
