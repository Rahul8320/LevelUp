// Implementation of undirected and un-weighted graph with the help of adjacency matrix

#include <bits/stdc++.h>
using namespace std;

void print_graph(vector<vector<int>> matrix, int n)
{
    for (int i = 1; i < n; i++)
    {
        cout << "Node: " << i << ", Neighbors: ";

        for (int j = 1; j < n; j++)
        {
            if (matrix[i][j] == 1)
            {
                cout << j << " ";
            }
        }

        cout << endl;
    }
}

int main()
{
    vector<vector<int>> edgeList = {{1, 2}, {2, 3}, {3, 4}, {4, 2}, {1, 3}};

    int n = 5;
    vector<vector<int>> adjacency_matrix(5, vector<int>(n, 0));

    for (int i = 0; i < edgeList.size(); i++)
    {
        // un-directed graph
        int a = edgeList[i][0];
        int b = edgeList[i][1];

        adjacency_matrix[a][b] = 1;
        adjacency_matrix[b][a] = 1;
    }

    print_graph(adjacency_matrix, n);
}