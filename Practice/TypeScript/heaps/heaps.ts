/// <reference path="../Scripts/typings/jquery/jquery.d.ts"/>

module Heaps {
    export class Heap {
        private _array: Array<number>;
        private _type: boolean;

        constructor(isMaxHeap: boolean) {
            this._array = new Array<number>();
            this._type = isMaxHeap;
        }

        public peek(index?: number): number {
            if (!index) {
                index = 0;
            }

            return this._array[index];
        }

        public push(value: number): void {
            var index: number = this._array.length;
            this._array[index] = value;
            this.heapifyUp(index);
        }

        public pop(): number {
            var toReturn = 0;
            var len = this._array.length;
            if (len > 0) {
                toReturn = this._array[0];
                this._array[0] = this._array[len - 1];
                this._array.pop();
                this.heapifyDown();
            }

            return toReturn;
        }

        private heapifyUp(index: number): void {
            if (index > 0) {
                var parent = Math.floor((index - 1) / 2);
                if (this._array[index] > this._array[parent]) {
                    this.swap(index, parent);

                    // heapify up the parents until we reach the root
                    // or the parent is larger than the new child.
                    this.heapifyUp(parent);
                }
            }
        }

        private heapifyDown(index?: number): void {
            if (!index) {
                index = 0;
            }

            var largest: number = index;
            var left: number = 2 * index + 1;
            var right: number = left + 1;

            var arr = this._array;
            var len: number = arr.length;

            if (left < len && arr[left] > arr[largest]) {
                largest = left;
            }

            if (right < len && arr[right] > arr[largest]) {
                largest = right;
            }

            if (largest !== index) {
                this.swap(index, largest);
                this.heapifyDown(largest);
            }
        }

        private swap(a: number, b: number): void {
            var tmp: number = this._array[a];
            this._array[a] = this._array[b];
            this._array[b] = tmp;
        }

        public toString(): string {
            var toReturn: string = "";
            var arr = this._array;
            for (var i = 0; i < arr.length; i++) {
                toReturn += arr[i] + ", ";
            }

            return toReturn;
        }
    }

    function prepareVisualization(content: HTMLElement): HTMLUListElement {
        content.innerHTML = "";
        var heapRoot = document.createElement("ul");
        content.appendChild(heapRoot);

        return heapRoot;
    }

    function visualize(heap: Heap, content: HTMLUListElement, index?: number) {
        if (!heap) {
            return;
        }

        if (!index) {
            index = 0;
        }

        var value = heap.peek(index);

        if (value || value === 0) {
            var newNode = document.createElement("li");
            newNode.textContent = value.toString();
            content.appendChild(newNode);

            var newNodeChildren = document.createElement("ul");
            newNode.appendChild(newNodeChildren);

            visualize(heap, newNodeChildren, 2 * index + 1);
            visualize(heap, newNodeChildren, 2 * index + 2);
        }
    }

    export function run() {
        var content = document.getElementById("content");
        var heap = new Heaps.Heap(true);

        heap.push(10);
        heap.push(11);
        heap.push(1);
        heap.push(2);
        heap.push(12);
        heap.push(5);
        heap.push(0);
        heap.push(-4);

        heap.pop();

        visualize(heap, prepareVisualization(content));
        //content.innerText = heap.toString();
    }
}

// run the tree visualization script.
$(document).ready(function () {
    Heaps.run();
});