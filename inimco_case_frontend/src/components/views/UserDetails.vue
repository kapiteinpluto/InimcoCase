<template>
  <h2 class="pageTitle">User details</h2>
  <div class="infobox col">
    <div class="row">
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
    <div class="row">
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
          <select v-model="account.Type">
            <option
              v-for="option in ['facebook', 'twitter', 'linkedin', 'other']"
              :key="option"
            >
              {{ option }}
            </option>
          </select>
          <input type="text" v-model="account.Address" />
          <button class="remove" @click="this.socialMedia.splice(index, 1)">
            -
          </button>
        </div>
        <button
          class="add"
          @click="this.socialMedia.push({ Type: '', Address: '' })"
        >
          Add
        </button>
      </div>
    </div>
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
// import axios from "axios";
import InputField from "../atoms/InputField.vue";

export default {
  components: { InputField },
  data() {
    return {
      firstName: "",
      lastName: "",
      skills: [],
      socialMedia: [],
    };
  },
  created() {},
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
  methods: {},
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
  border: 0.1rem solid blue;
  border-radius: 2rem;
  width: 90%;
  max-width: 80rem;
  margin: auto;
  .profilePicture {
    width: 10rem;
    margin: 0.5rem 1rem;
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
