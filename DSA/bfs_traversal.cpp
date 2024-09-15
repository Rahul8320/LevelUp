// Breadth First Search traversal in graph

#include <bits/stdc++.h>
using namespace std;

void bfs_traversal(int source, unordered_map<int, vector<int>> graph, int nodes)
{
    queue<int> q;
    vector<int> visited_nodes(nodes + 1);

    q.push(source);
    visited_nodes[source] = 1;

    while (!q.empty())
    {
        int front_node = q.front();
        q.pop();

        cout << front_node << " ";

        for (int nbr : graph[front_node])
        {
            if (!visited_nodes[nbr])
            {
                visited_nodes[nbr] = 1;
                q.push(nbr);
            }
        }
    }

    cout << endl;
}

int main()
{
    vector<vector<int>> edgeList = {{0, 1}, {1, 4}, {1, 2}, {2, 3}};

    int nodes = 5;

    unordered_map<int, vector<int>> graph;

    for (int i = 0; i < edgeList.size(); i++)
    {
        // un-directed graph
        int a = edgeList[i][0];
        int b = edgeList[i][1];

        graph[a].push_back(b);
        graph[b].push_back(a);
    }

    bfs_traversal(edgeList[0][0], graph, nodes);
}