========================================
MUNICIPAL SERVICES APPLICATION
Student: ST10465088
Course: AAPD7112 - POE Part 1, 2, 3
Date: 2026
========================================
# Municipal Services Application

## South Africa - Community Service Platform

### Overview
The Municipal Services Application is a comprehensive Windows Forms application designed to help South African citizens report municipal issues, view local events, and track service request status.

### Features

#### 1. Report Issues (Part 1)
- Submit service requests with location, category, description
- Attach files (images, documents)
- Progress bar with gamification
- Unique Request ID generation
- Data persistence using Dictionary

#### 2. Local Events (Part 2)
- Browse upcoming events
- Search by keyword
- Filter by category
- Personalized recommendations using PriorityQueue
- SortedDictionary for date ordering

#### 3. Service Status (Part 3)
- View all submitted requests
- Track request status by ID (AVL Tree)
- Priority queue processing (MinHeap)
- Related requests via Graph BFS
- Status updates


### Installation

#### Prerequisites
- Visual Studio 2019 or 2022
- .NET Framework 4.7.2 or higher
- Windows 10/11

#### Steps
1. Extract the source code zip file
2. Open MunicipalServicesApp.sln in Visual Studio
3. Build the solution (Ctrl+Shift+B)
4. Press F5 to run

### Usage Guide

#### Main Menu
- **Report Issues** - Submit a new service request
- **Local Events** - View and search events
- **Service Status** - Track request progress
- **Exit** - Close the application

#### Report Issues
1. Enter location (required)
2. Select category (required)
3. Add description (required)
4. Attach optional file
5. Click Submit
6. Save your Request ID

#### Local Events
1. Browse events in the grid
2. Search by keyword (press Enter)
3. Filter by category dropdown
4. View recommendations in sidebar

#### Service Status
1. View all requests in the grid
2. Click a row to see details
3. Search by Request ID (AVL Tree)
4. View priority queue (MinHeap)
5. See related requests (Graph BFS)
6. Update status from dropdown


### Troubleshooting

#### Application won't compile
- Ensure all .cs files are included in project
- Check .NET Framework version matches
- Verify all references are resolved

#### Requests not showing
- Submit at least one request first
- Refresh the Service Status form

#### File attachment not working
- Ensure file path is valid
- Check file permissions

### Performance
- AVL Tree search: < 10ms for 1000+ records
- MinHeap operations: O(log n)
- Graph BFS: O(V+E)
- Dictionary lookup: O(1)

### Contact
Student: ST10465088
Course: AAPD7112
