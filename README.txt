Java Console implementation of Conway's game of life

Rules:

1.Check all 8 surround neighbors from a cell
2. The cell will live iff neighbors = 2 or 3
3. The cell will die iff neighbours <=1 or >=4
4. The cell will become alive iff neighbours==3
5. The cell will stay dead iff neighbours does not ==3

Configuration tests Glider and Blinker

To compile: javac Life.java
To run: java Life
