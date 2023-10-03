<template>
  <h2 class="pageTitle">User details</h2>
  <div class="infobox col">
    <div class="row basicInfo">
      <img
        class="profilePicture"
        src="@/assets/profile.png"
        alt="profile picture"
      />
      <div class="namebox">
        <InputField v-model="firstName" inputName="firstname"></InputField>
        <InputField v-model="lastName" inputName="lastname"></InputField>
      </div>
    </div>
    <div class="row detailInfo">
      <div class="col">
        <h3>Social skills</h3>
        <div v-for="(skill, index) in skills" :key="index">
          <input type="text" v-model="skills[index]" />
          <button class="remove" @click="skills.splice(index, 1)">-</button>
        </div>
        <button class="add" @click="this.skills.push('')">Add</button>
      </div>
      <div class="col">
        <h3>Socialmedia</h3>
        <div
          class="socialmediaInputs"
          v-for="(account, index) in socialMedia"
          :key="index"
        >
          <select v-model="account.type">
            <option
              v-for="option in ['facebook', 'twitter', 'linkedin', 'other']"
              :key="option"
            >
              {{ option }}
            </option>
          </select>
          <input type="text" v-model="account.address" />
          <button class="remove" @click="this.socialMedia.splice(index, 1)">
            -
          </button>
        </div>
        <button
          class="add"
          @click="this.socialMedia.push({ type: '', address: '' })"
        >
          Add
        </button>
      </div>
    </div>
    <button @click="setDetails()">Save</button>
  </div>
  <div class="feedbackbox">
    <p>The number of vowels: {{ vowelCount }}</p>
    <p>The number of constenants: {{ constenantCount }}</p>
    <p>The reverse full name: {{ reverseName }}</p>
    <p>JSON:</p>
    <pre class="jsonView">{{ json }}</pre>
  </div>
</template>

<script>
import axios from "axios";
import InputField from "../atoms/InputField.vue";

export default {
  components: { InputField },
  data() {
    return {
      id: 1,
      firstName: "",
      lastName: "",
      skills: [],
      socialMedia: [],
    };
  },
  created() {
    this.getDetails();
  },
  computed: {
    vowelCount() {
      const vowels = ["a", "e", "i", "o", "u"];
      let count = 0;
      (this.firstName + this.lastName)
        .toLowerCase()
        .split("")
        .forEach((letter) => {
          if (vowels.includes(letter)) {
            count++;
          }
        });
      return count;
    },
    constenantCount() {
      return (this.firstName + this.lastName).length - this.vowelCount;
    },
    reverseName() {
      return (this.firstName + " " + this.lastName)
        .split("")
        .reverse()
        .join("");
    },
    json() {
      return JSON.stringify(this.$data, null, 2);
    },
  },
  methods: {
    getDetails() {
      const url = "http://localhost:5236/user/" + this.id;
      axios.get(url).then((res) => {
        this.id = res.data.id;
        this.firstName = res.data.firstName;
        this.lastName = res.data.lastName;
        this.skills = res.data.skills;
        this.socialMedia = res.data.socialMedia;
      });
    },
    setDetails() {
      const url = "http://localhost:5236/user/" + this.id;
      axios.put(url, this.$data).then(() => {
        console.log("saved data");
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.col {
  display: flex;
  flex-direction: column;
}
.row {
  display: flex;
  flex-direction: row;
}

.pageTitle {
  font-size: 2rem;
}

.infobox {
  border: 0.1rem solid rgb(0, 0, 0);
  border-radius: 2rem;
  width: 90%;
  max-width: 80rem;
  margin: auto;
  background: rgb(171, 235, 243);

  .basicInfo {
    display: flex;
    justify-content: center;
    flex-wrap: wrap;
    .profilePicture {
      width: 10rem;
      padding: 0.5rem 2rem;
      background: white;
    }
    .namebox {
      display: flex;
      flex-direction: column;
      justify-content: space-evenly;
      background: white;
    }
  }
  .detailInfo {
    display: flex;
    margin: 5%;
    justify-content: space-around;
    flex-wrap: wrap;
    background: white;
    div {
      width: 45%;
      min-width: 20rem;
    }
    .add {
      width: 5rem;
      margin: auto;
    }
  }
}

.feedbackbox {
  .jsonView {
    text-align: left;
    margin: auto;
    width: 15rem;
  }
}
</style>
