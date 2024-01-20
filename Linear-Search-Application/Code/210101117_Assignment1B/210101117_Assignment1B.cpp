/* 210101117_Assignment1B.cpp By Dhanesh V, 210101117 */
/* This is an implementation of the Linear Search Application without graphics and feature enhancements in C++ */

#include "stdafx.h"
#include <iostream>
#include <stdio.h>
#include <fstream>
#include <vector>
#include <string>
#include <sstream>
#include <algorithm>
using namespace std;

/**
 * @brief Perform linear search on a vector.
 *
 * This function searches for a key in a vector of strings using linear search.
 *
 * @param data Vector containing the data to be searched.
 * @param key The key to be searched in the vector.
 * @return A pair indicating whether the key is found (true/false) and the index of its first occurrence in the vector.
 */
pair<bool, int> linearSearch(vector<string>& data, string& key) 
{
    /*
        Input: vector containing the data and key to be searched
        Output: {true, first index of occurrence of key} if the element is found, otherwise {false,-1}
    */

    for (int i = 0; i < data.size(); i++)
    {
        // Compare if the element at i'th index in the list is the same as the key
        if (data[i] == key)
        {
            // return true and the index i
            return make_pair(true, i);
        }
    }

    // If the element is not found, return false with a dummy index, -1
    return make_pair(false, -1);
}

/**
 * @brief Normalize the string representation of a floating-point number.
 *
 * This function normalizes the string representation of a floating-point number
 * by removing trailing and leading zeroes. If the input is not a floating-point
 * number, the original string is returned.
 *
 * @param input The input string to be normalized.
 * @return Normalized string removing trailing and leading zeroes if it's a floating-point number; otherwise, the original string itself.
 */
string normalizeFloatString(const string& input) 
{
    /*
        Input: string
        Output: Normalized string removing trailing and leading zeroes if it's a floating-point number, otherwise, the original string itself.
        Eg: inputs like "3.500" will be returned as "3.5" so that they can be compared easily for linear search
    */
    istringstream iss(input); // Making the input string stream
    double value;
    if (iss >> value)
    {
        ostringstream oss; // Making the output string stream
        oss << value;
        // If the value is a floating-point number, return the normalized string
        return oss.str();
    }
    // If it's not a floating-point number, return the original string
    return input;
}

/**
 * @brief Main function to read data from a file, perform linear search, and display the results.
 *
 * This function orchestrates the entire application, reading data from a file, displaying the elements,
 * obtaining user input, and performing a linear search.
 *
 * @return 0 on successful execution.
 */
int main() 
{
    vector<string> inputData; // Vector to store data from file
    FILE* file = nullptr; // Initialsing the file pointer
	char line[256]; // To read the data from the input file
	pair<bool,int> searchResult; bool found = false; int index; // To store the searchresult after linear search
	string userInput; // To get the userinput to be searched

    if (fopen_s(&file, "input.txt", "r") != 0) 
    {
        // Print in the error stream if the file is not openable
        cerr << "Error opening the file. Make sure that \"input.txt\" is in same folder as this cpp file." << endl;
		system("pause");
        return 1; 
    }

    // Read from the file until EOF is obtained
    while (fscanf_s(file, "%255s", line, static_cast<unsigned>(_countof(line))) != EOF)
    {
        // Push the data into the vector after normalizing
        inputData.push_back(normalizeFloatString(line));
    }

    // Closing the file after reading it
    fclose(file);

    // Check if the file is empty and give an error message
    if (inputData.size() == 0)
    {
        cerr << "The file is empty. Enter some elements into the file" << endl;
        return 1;
    }

    // Printing the elements of the list read from the file
    cout << "The elements of the list are: " << endl;

    for (int i = 0;  i < inputData.size(); i++) 
    {
        if (i != inputData.size() - 1)
        {
            cout << inputData[i] << ", ";
        }
        else
        {
            cout << inputData[i];
        }
    }
    cout << endl;

    // Prompt user for input and obtain the input
    cout << "Enter the element to be searched: ";
    cin >> userInput;

    // Handle float cases by normalizing
    userInput = normalizeFloatString(userInput);

    // Perform linear search using the linear search function and store its result
    searchResult = linearSearch(inputData, userInput);
    found = searchResult.first;
    index = searchResult.second;

    // Print the result along with the index if it is found
    if (found)
    {
        cout << "Element found at index " << index << endl;
    } 
    else
    {
        cout << "Element not found." << endl;
    }

    // Added a delay before closing the console window for Visual Studio Code
    system("pause");

    return 0;
}
