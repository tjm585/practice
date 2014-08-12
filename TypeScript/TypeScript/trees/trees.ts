/// <reference path="../Scripts/typings/jquery/jquery.d.ts"/>

module Trees {
    export class Tree {
        root: Node;

        public insert(value: number): void {
            if (!this.root) {
                this.root = new Node(value, null);
            }
            else {
                this._insertHelper(value, this.root);
            }
        }

        private _insertHelper(value: number, node: Node) {
            if (node.value > value) {
                if (!node.left) {
                    node.left = new Node(value, node);
                }
                else {
                    this._insertHelper(value, node.left);
                }
            }
            else if (node.value < value) {
                if (!node.right) {
                    node.right = new Node(value, node);
                }
                else {
                    this._insertHelper(value, node.right);
                }
            }
            else {
                throw "equal case not handled";
            }
        }

        private _replaceInParent(node: Node, replacement: Node): void {
            var parent = node.parent;
            if (parent.left && node.value === parent.left.value) {
                parent.left = replacement;
            }
            else if (parent.right && node.value === parent.right.value) {
                parent.right = replacement;
            }

            if (replacement) {
                replacement.parent = parent;
            }
        }

        public del(value: number, node?: Node): void {
            node = (!node) ? this.root : node;

            if (!node) {
                return;
            }

            if (node.value === value) {
                if (!node.left && !node.right) {
                    // no child case.
                    this._replaceInParent(node, null);
                }
                else if (node.left && !node.right) {
                    // only left child.
                    this._replaceInParent(node, node.left);
                }
                else if (node.right && !node.left) {
                    // only right child.
                    this._replaceInParent(node, node.right);
                }
                else {
                    // both children.
                    var left = node.left;
                    node.value = node.left.value;
                    this.del(node.value, node.left);
                }
            }
            else {
                // traverse the tree
                if (node.value > value) {
                    this.del(value, node.left);
                }
                else {
                    this.del(value, node.right);
                }
            }
        }
    }

    export class Node {
        value: number;
        left: Node;
        right: Node;
        parent: Node;

        constructor(value: number, parent: Node) {
            this.value = value;
            this.parent = parent;
        }
    }
}

function prepareVisualization(content: HTMLElement): HTMLUListElement {
    content.innerHTML = "";
    var treeRoot = document.createElement("ul");
    content.appendChild(treeRoot);

    return treeRoot;
}

function visualize(node: Trees.Node, content: HTMLUListElement, isLeft?: boolean) {
    if (!node) {
        return;
    }

    // insert the value of the node.
    var color = (isLeft === undefined) ? "root" : (isLeft) ? "left" : "right";
    var li = document.createElement("li");
    li.innerText = node.value.toString();
    li.classList.add(color);
    content.appendChild(li);

    // allow the child to be a parent.
    var ul = document.createElement("ul");
    li.appendChild(ul);

    visualize(node.left, ul, true);
    visualize(node.right, ul, false);
}

function run() {
    var content = document.getElementById("content");

    var tree = new Trees.Tree();
    tree.insert(5);
    tree.insert(10);
    tree.insert(4);
    tree.insert(3);
    tree.insert(11);
    tree.insert(12);
    tree.insert(8);

    tree.del(10);
    tree.del(5);
    tree.insert(5);

    visualize(tree.root, prepareVisualization(content));
}

// run the tree visualization script.
$(document).ready(function () {
    run();
});