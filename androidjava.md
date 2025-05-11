### SECTION B ANSWERS  

---

#### **QUESTION 1: Android Studio Basics**  
**a. Role of AndroidManifest.xml**  
The `AndroidManifest.xml` file is essential for configuring an Android app. It declares the app’s components (e.g., activities, services, broadcast receivers), permissions (e.g., internet access, camera), and metadata like the app’s package name and version. It also specifies the app’s minimum API level and defines launcher activities.  

**b. Steps to create a RecyclerView**  
1. **Add Dependency**: Include RecyclerView in `build.gradle`:  
   ```gradle  
   implementation 'androidx.recyclerview:recyclerview:1.2.1'  
   ```  
2. **Define Layout**: Add RecyclerView to XML layout (e.g., `activity_main.xml`).  
3. **Create Item Layout**: Design the item UI (e.g., `item_view.xml`).  
4. **Create Adapter**: Extend `RecyclerView.Adapter` and define a `ViewHolder`:  
   ```java  
   public class MyAdapter extends RecyclerView.Adapter<MyAdapter.ViewHolder> {  
       private List<String> data;  
       public MyAdapter(List<String> data) { this.data = data; }  

       @Override  
       public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {  
           View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_view, parent, false);  
           return new ViewHolder(view);  
       }  

       @Override  
       public void onBindViewHolder(ViewHolder holder, int position) {  
           holder.textView.setText(data.get(position));  
       }  

       static class ViewHolder extends RecyclerView.ViewHolder {  
           TextView textView;  
           public ViewHolder(View itemView) {  
               super(itemView);  
               textView = itemView.findViewById(R.id.item_text);  
           }  
       }  
   }  
   ```  
5. **Set LayoutManager and Adapter** in the activity:  
   ```java  
   RecyclerView recyclerView = findViewById(R.id.recyclerView);  
   recyclerView.setLayoutManager(new LinearLayoutManager(this));  
   recyclerView.setAdapter(new MyAdapter(dataList));  
   ```  

**c. Intent Example**  
Intents facilitate communication between Android components. Example:  
```java  
// Explicit Intent to start AnotherActivity  
Intent intent = new Intent(MainActivity.this, AnotherActivity.class);  
intent.putExtra("key", "value"); // Passing data  
startActivity(intent);  
```  
**Purpose**: Used to start activities, services, or broadcast messages.  

---

#### **QUESTION 2: Java and Retrofit**  
**a. Retrofit Purpose and Advantages**  
Retrofit simplifies REST API communication by converting HTTP API into Java interfaces. Advantages:  
- Type-safe requests/responses.  
- Built-in support for JSON parsing (e.g., Gson).  
- Reduces boilerplate code.  
- Supports synchronous/asynchronous calls.  

**b. Retrofit Interface and Instance**  
```java  
// Interface  
public interface ApiService {  
    @GET("users")  
    Call<List<User>> getUsers();  
}  

// Retrofit Instance  
public class RetrofitClient {  
    private static Retrofit retrofit = null;  
    public static ApiService getClient() {  
        if (retrofit == null) {  
            retrofit = new Retrofit.Builder()  
                .baseUrl("https://api.example.com/")  
                .addConverterFactory(GsonConverterFactory.create())  
                .build();  
        }  
        return retrofit.create(ApiService.class);  
    }  
}  

// API Call (in Activity)  
ApiService service = RetrofitClient.getClient();  
Call<List<User>> call = service.getUsers();  
call.enqueue(new Callback<List<User>>() {  
    @Override  
    public void onResponse(Call<List<User>> call, Response<List<User>> response) {  
        // Handle response  
    }  
    @Override  
    public void onFailure(Call<List<User>> call, Throwable t) {  
        // Handle error  
    }  
});  
```  

**c. JSON Parsing with Gson**  
Retrofit uses `GsonConverterFactory` to deserialize JSON into Java objects. Ensure model classes match JSON structure:  
```java  
public class User {  
    @SerializedName("id")  
    private int id;  
    // Getters and setters  
}  
```  

---

#### **QUESTION 3: MySQL and Database Connectivity**  
**a. Steps to Connect via Retrofit**  
1. **Backend Setup**: Create a REST API (e.g., using PHP/Node.js) to interact with MySQL.  
2. **Retrofit in Android**: Define Retrofit interface for API endpoints.  
3. **Model Classes**: Create POJOs matching the JSON structure.  
4. **Permissions**: Add internet permission in `AndroidManifest.xml`.  

**b. SQL Command**  
```sql  
CREATE TABLE users (  
    id INT AUTO_INCREMENT PRIMARY KEY,  
    name VARCHAR(255) NOT NULL,  
    email VARCHAR(255) NOT NULL UNIQUE,  
    password VARCHAR(255) NOT NULL  
);  
```  

**c. Prepared Statements vs Regular Queries**  
- **Prepared Statements**: Parameterized, prevent SQL injection, reusable.  
- **Regular Queries**: Concatenated strings, prone to injection, less efficient.  

---

#### **QUESTION 4: MVVM Architecture**  
**a. Components**  
- **Model**: Manages data (e.g., database, network).  
- **View**: Displays UI and observes ViewModel.  
- **ViewModel**: Exposes data streams to View, survives configuration changes.  

**b. ViewModel Code**  
```java  
public class MyViewModel extends ViewModel {  
    private MutableLiveData<String> data = new MutableLiveData<>();  

    public LiveData<String> getData() {  
        return data;  
    }  

    public void loadData() {  
        // Fetch data from repository  
        data.setValue("Hello MVVM");  
    }  
}  
```  

**c. LiveData for UI Updates**  
LiveData is lifecycle-aware, ensuring UI updates only when the activity/fragment is active. It automatically handles configuration changes (e.g., screen rotation).  

---  
**Bismark Atta FRIMPONG**  
Page 3 of 3
